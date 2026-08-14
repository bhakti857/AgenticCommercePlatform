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

var provider = services.BuildServiceProvider();
var agent = provider.GetRequiredService<AgentHarness>();

// 👇 NEW CODE GOES HERE — right after the provider/agent are built,
//    right before the console UI starts.
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

string? sessionId = Guid.NewGuid().ToString();
string? userId = "cli-user";

while (true)
{
    Console.Write("🤖 > ");
    var input = Console.ReadLine();
    if (string.IsNullOrEmpty(input) || input.ToLower() == "exit")
        break;

    try
    {
        var response = await agent.ProcessMessageAsync(userId, input, sessionId);
        Console.WriteLine($"\n{response}\n");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

Console.WriteLine("Goodbye!");