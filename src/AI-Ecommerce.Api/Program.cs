using AI_Ecommerce.Agent.Harness;
using AI_Ecommerce.Agent.Tools;
using AI_Ecommerce.Api.Services;
using AI_Ecommerce.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.AI;
using Microsoft.IdentityModel.Tokens;
using OpenAI;
using System.ClientModel;
using System.Text;
using System.Text.Json.Serialization;
using DotNetEnv;

Env.Load("../../.env");

var builder = WebApplication.CreateBuilder(args);

// 1. Add Controllers & Swagger
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]))
        };
    });

// 5. Add Authorization
builder.Services.AddAuthorization();

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

builder.Services.AddScoped<IChatClient>(sp =>
{
    var groqKey = Environment.GetEnvironmentVariable("GROQ_API_KEY");

    if (string.IsNullOrEmpty(groqKey))
    {
        Console.WriteLine("⚠️  GROQ_API_KEY not set – using mock client.");
        return new MockChatClient();
    }

    Console.WriteLine("✅ Using Groq (Llama 3.3 70B)");
    var credential = new ApiKeyCredential(groqKey);
    var options = new OpenAIClientOptions
    {
        Endpoint = new Uri("https://api.groq.com/openai/v1")
    };
    var client = new OpenAIClient(credential, options);
    IChatClient chatClient = client
        .GetChatClient("llama-3.3-70b-versatile")
        .AsIChatClient();

    return new ChatClientBuilder(chatClient)
        .UseFunctionInvocation()
        .Build();
});

var app = builder.Build();

// ⚠️ TEMPORARY: auto-approve all writes/commands from the web API.
// The console-based y/n approval flow doesn't translate to concurrent HTTP requests.
// TODO: replace with a proper "pending approval" workflow (e.g., return a
// confirmation token to the frontend, require a follow-up call to execute).
DevTools.ApprovalHandler = async (description) =>
{
    Console.WriteLine($"⚠️  Auto-approved (web API, no interactive gate): {description}");
    return await Task.FromResult(true);
};

// 7. Configure Middleware Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowReactApp");

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// 8. Seed Database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await DataSeeder.SeedAsync(dbContext);
}

app.Run();