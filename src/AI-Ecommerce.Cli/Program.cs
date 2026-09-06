using AI_Ecommerce.Agent.Harness;
using AI_Ecommerce.Agent.Tools;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenAI;
using System.ClientModel;
using DotNetEnv;
using AI_Ecommerce.Data;
using Microsoft.EntityFrameworkCore;
using AI_Ecommerce.Cli.OpenCode;
Env.Load("../../.env");

var services = new ServiceCollection();
services.AddLogging(builder => builder.AddConsole());
var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING")
    ?? "Server=(localdb)\\mssqllocaldb;Database=AI-Ecommerce;Trusted_Connection=True;";

services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString)
           .LogTo(_ => { }, LogLevel.None));

services.AddScoped<AgentHarness>();

services.AddScoped<IChatClient>(sp =>
{
    var orKey = Environment.GetEnvironmentVariable("OPENROUTER_API_KEY");

    if (string.IsNullOrEmpty(orKey))
    {
        Console.WriteLine("⚠️  OPENROUTER_API_KEY not set – using mock client.");
        return new MockChatClient();
    }

    Console.WriteLine("✅ Using OpenRouter (Llama 3.3 70B, free tier)");
    var credential = new ApiKeyCredential(orKey);
    var options = new OpenAIClientOptions
    {
        Endpoint = new Uri("https://openrouter.ai/api/v1")
    };
    var client = new OpenAIClient(credential, options);
    IChatClient chatClient = client
        .GetChatClient("openrouter/free")
        .AsIChatClient();

    return new ChatClientBuilder(chatClient)
        .UseFunctionInvocation()
        .Build();
});

string providerName = (Environment.GetEnvironmentVariable("LLM_PROVIDER") ?? "opencode").ToLowerInvariant();

if (providerName == "opencode")
{
    await RunOpenCodeChatAsync();
}
else
{
    var provider = services.BuildServiceProvider();
    var agent = provider.GetRequiredService<AgentHarness>();
    await RunOpenRouterChatAsync(agent);
}

static async Task RunOpenCodeChatAsync()
{
    var baseUrl = Environment.GetEnvironmentVariable("OPENCODE_URL") ?? "http://127.0.0.1:4096";
    var password = Environment.GetEnvironmentVariable("OPENCODE_SERVER_PASSWORD");
    var client = new OpenCodeClient(baseUrl, password);

    if (!await client.IsHealthyAsync())
    {
        Console.WriteLine($"⚠️  opencode server not reachable at {baseUrl}.");
        Console.WriteLine("Start it in another terminal with:");
        Console.WriteLine("    opencode serve --port 4096");
        Console.WriteLine("or set LLM_PROVIDER=openrouter to use OpenRouter instead.");
        return;
    }

    // Resume the last session so chat history is maintained across restarts.
    var sessionId = SessionStore.Load();
    if (!string.IsNullOrEmpty(sessionId))
        Console.WriteLine($"💬 Resuming conversation (session {sessionId})");
    else
    {
        sessionId = await client.CreateSessionAsync();
        SessionStore.Save(sessionId);
        Console.WriteLine($"🔵 New opencode session: {sessionId}");
    }

    Console.WriteLine($"✅ Connected to opencode server at {baseUrl}");
    Console.WriteLine("🧠 Agentic Development Assistant (opencode)");
    Console.WriteLine("Type your commands (type 'exit' to quit)");
    Console.WriteLine();

    while (true)
    {
        Console.Write("🤖 > ");
        var input = Console.ReadLine();
        if (string.IsNullOrEmpty(input) || input.ToLower() == "exit")
            break;

        try
        {
            var response = await client.SendMessageAsync(sessionId, input);
            Console.WriteLine($"\n{response}\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    Console.WriteLine("Goodbye!");
}

static async Task RunOpenRouterChatAsync(AgentHarness agent)
{
    // 👇 Approval gating for the interactive OpenRouter harness path.
    DevTools.ApprovalHandler = async (description) =>
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"⚠️  Approval needed: {description}");
        Console.ResetColor();
        Console.Write("Proceed? (y/n): ");

        var input = Console.ReadLine();
        return await Task.FromResult(
            !string.IsNullOrEmpty(input) &&
            (input.Trim().ToLower() == "y" || input.Trim().ToLower() == "yes")
        );
    };

    Console.WriteLine("🧠 Agentic Development Assistant");
    Console.WriteLine("Type your commands (type 'exit' to quit)");
    Console.WriteLine();

    var sessionId = Guid.NewGuid().ToString();
    var userId = "cli-user";

    while (true)
    {
        Console.Write("🤖 > ");
        var input = Console.ReadLine();
        if (string.IsNullOrEmpty(input) || input.ToLower() == "exit")
            break;

        try
        {
            var response = await agent.ProcessMessageAsync(userId, input, sessionId, allowWriteTools: true);
            Console.WriteLine($"\n{response}\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    Console.WriteLine("Goodbye!");
}