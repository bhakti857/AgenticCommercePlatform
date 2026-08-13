using AI_Ecommerce.Agent.Harness;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenAI;
using System.ClientModel;
using DotNetEnv;

Env.Load("../../.env");

var services = new ServiceCollection();

services.AddLogging(builder => builder.AddConsole());
services.AddScoped<AgentHarness>();

services.AddScoped<IChatClient>(sp =>
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
        .GetChatClient("llama-3.1-8b-instant")
        .AsIChatClient();

    return new ChatClientBuilder(chatClient)
        .UseFunctionInvocation()
        .Build();
});

var provider = services.BuildServiceProvider();
var agent = provider.GetRequiredService<AgentHarness>();

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