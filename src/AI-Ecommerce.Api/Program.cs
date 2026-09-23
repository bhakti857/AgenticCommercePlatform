using AI_Ecommerce.Agent.Harness;
using AI_Ecommerce.Agent.Tools;
using AI_Ecommerce.Api.Services;
using AI_Ecommerce.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.AI;
using Microsoft.IdentityModel.Tokens;
using OpenAI;
using System.ClientModel;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;
using DotNetEnv;

Env.Load("../../.env");

var builder = WebApplication.CreateBuilder(args);

// Prefer JWT_SECRET from .env / environment over any value baked into
// appsettings.json — the secret must never be committed to source control.
var jwtSecretFromEnv = Environment.GetEnvironmentVariable("JWT_SECRET");
if (!string.IsNullOrWhiteSpace(jwtSecretFromEnv))
{
    builder.Configuration["Jwt:Secret"] = jwtSecretFromEnv;
}

var jwtSecret = builder.Configuration["Jwt:Secret"];
if (string.IsNullOrWhiteSpace(jwtSecret) || jwtSecret.Length < 32)
{
    throw new InvalidOperationException(
        "Jwt:Secret is missing or too short (needs 32+ characters). " +
        "Set JWT_SECRET in your .env file (see .env.example) — do not hardcode it in appsettings.json.");
}

// 1. Add Controllers & Swagger
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 1b. Global exception handling — RFC 7807 ProblemDetails instead of raw 500s
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

// 2. Register DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 3. Register JwtService
builder.Services.AddScoped<JwtService>();

// 4. Add JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSecret))
        };
    });

// 5. Add Authorization — user-type policies so controllers can use
// [Authorize(Policy = ...)] instead of re-parsing claims ad hoc.
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireEmployee", policy => policy.RequireClaim("AccountType", "Employee"));
    options.AddPolicy("MasterAdminOnly", policy => policy.RequireClaim("UserTypeId", "1"));
    options.AddPolicy("MasterAdminOrAdmin", policy => policy.RequireClaim("UserTypeId", "1", "2"));
});

// 5b. Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials();
        });
});

// 6. Register Agent Services
builder.Services.AddScoped<AgentHarness>();

// 6a. Agent approval gate — in-memory store of pending WriteFile/ExecuteCommand
// operations awaiting an explicit Approve/Deny decision (see ApprovalGate.cs).
builder.Services.AddSingleton<ApprovalGate>();

// 6b. Rate limiting — protect the login endpoint from brute-force attempts
// (partitioned per client IP) and the agent chat endpoint from being spammed
// (partitioned per authenticated user, falling back to IP for anonymous
// callers). Both use a fixed window with no queueing — excess requests are
// rejected immediately with 429 rather than delayed.
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    options.AddPolicy("auth", httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 5,
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 0
            }));

    options.AddPolicy("agent-chat", httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? httpContext.Connection.RemoteIpAddress?.ToString()
                ?? "unknown",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 10,
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 0
            }));

    options.OnRejected = async (context, cancellationToken) =>
    {
        context.HttpContext.Response.ContentType = "application/json";
        await context.HttpContext.Response.WriteAsync(
            "{\"error\":\"Too many requests. Please try again later.\"}", cancellationToken);
    };
});

// Chat client: Groq primary with OpenRouter auto-fallback on 429/404.
builder.Services.AddScoped<IChatClient>(sp =>
{
    var groqKey = Environment.GetEnvironmentVariable("GROQ_API_KEY");
    var openRouterKey = Environment.GetEnvironmentVariable("OPENROUTER_API_KEY");

    IChatClient? groq = null;
    if (!string.IsNullOrEmpty(groqKey))
    {
        var groqModel = Environment.GetEnvironmentVariable("GROQ_MODEL") ?? "openai/gpt-oss-20b";
        groq = new OpenAIClient(
                new ApiKeyCredential(groqKey),
                new OpenAIClientOptions { Endpoint = new Uri("https://api.groq.com/openai/v1") })
            .GetChatClient(groqModel)
            .AsIChatClient();
        Console.WriteLine($"✅ Using Groq ({groqModel})");
    }

    IChatClient? openRouter = null;
    if (!string.IsNullOrEmpty(openRouterKey))
    {
        // openrouter/free auto-routes to whatever free model is currently online,
        // so this fallback doesn't rot when a specific free model is delisted.
        openRouter = new OpenAIClient(
                new ApiKeyCredential(openRouterKey),
                new OpenAIClientOptions { Endpoint = new Uri("https://openrouter.ai/api/v1") })
            .GetChatClient("openrouter/free")
            .AsIChatClient();
        Console.WriteLine("✅ Using OpenRouter (openrouter/free) as fallback");
    }

    if (groq == null && openRouter == null)
    {
        Console.WriteLine("⚠️  GROQ/OPENROUTER key not set – using mock client.");
        return new MockChatClient();
    }

    IChatClient effective = groq != null && openRouter != null
        ? new FallbackChatClient(groq, openRouter, sp.GetRequiredService<ILogger<FallbackChatClient>>())
        : groq ?? openRouter ?? new MockChatClient();

    return new ChatClientBuilder(effective)
        .UseFunctionInvocation()
        .Build();
});

// G5: bounded ConversationHistory – purge rows older than 90 days daily.
builder.Services.AddHostedService<ConversationHistoryCleanupService>();

var app = builder.Build();

// Agent write/execute tools no longer auto-approve. Each WriteFile/ExecuteCommand
// parks until an operator resolves it via POST /api/agent/approvals/{token} —
// pending items are listed by GET /api/agent/approvals. Unresolved approvals
// auto-deny after 10 minutes so a conversation can't hang forever.
var approvalGate = app.Services.GetRequiredService<ApprovalGate>();
DevTools.ApprovalHandler = async (description) =>
{
    var token = approvalGate.Submit(description);
    Console.WriteLine($"⏳ Approval required ({token}): {description}");
    try
    {
        return await approvalGate.WaitForDecisionAsync(token).WaitAsync(TimeSpan.FromMinutes(10));
    }
    catch (TimeoutException)
    {
        Console.WriteLine($"⏳ Approval {token} timed out — denied.");
        return false;
    }
};

// 7. Configure Middleware Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseCors("AllowReactApp");

app.UseAuthentication();
app.UseAuthorization();
app.UseRateLimiter();
app.MapControllers();

// 8. Seed Database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await DataSeeder.SeedAsync(dbContext);
}

app.Run();