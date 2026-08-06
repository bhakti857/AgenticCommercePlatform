using AI_Ecommerce.Agent.Harness;
using Microsoft.Extensions.AI;  
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenAI;
using System.ClientModel;

var services = new ServiceCollection();

services.AddLogging(builder => builder.AddConsole());
services.AddScoped<AgentHarness>();

services.AddScoped<IChatClient>(sp =>
{
    var token = Environment.GetEnvironmentVariable("GITHUB_TOKEN");
    if (string.IsNullOrEmpty(token))
    {
        Console.WriteLine("⚠️  GITHUB_TOKEN not set – using mock client.");
        return new MockChatClient();
    }

    Console.WriteLine("✅ Using GitHub Models (gpt-4o-mini)");

    // ✅ Correct order: credential first, then options with Endpoint set
    var credential = new ApiKeyCredential(token);
    var options = new OpenAIClientOptions
    {
        Endpoint = new Uri("https://models.inference.ai.azure.com")
    };
    var client = new OpenAIClient(credential, options);
    return client.AsChatClient("gpt-4o-mini");
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