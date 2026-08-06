using AI_Ecommerce.Agent.Tools;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Logging;

namespace AI_Ecommerce.Agent.Harness
{
    public class AgentHarness
    {
        private readonly IChatClient _chatClient;
        private readonly ILogger<AgentHarness> _logger;
        private readonly Dictionary<string, List<ChatMessage>> _sessionHistory = new();

        public AgentHarness(IChatClient chatClient, ILogger<AgentHarness> logger)
        {
            _chatClient = chatClient;
            _logger = logger;
        }
        public async Task<string> ProcessMessageAsync(string userId, string message, string sessionId)
        {
            try
            {
                if (!_sessionHistory.TryGetValue(sessionId, out var history))
                {
                    history = new List<ChatMessage>
            {
                new ChatMessage(ChatRole.System, GetSystemPrompt(userId))
            };
                    _sessionHistory[sessionId] = history;
                }

                history.Add(new ChatMessage(ChatRole.User, message));

                // Create ChatOptions with tools
                var chatOptions = new ChatOptions
                {
                    Tools = GetAgentTools().Cast<AITool>().ToList(),
                    MaxOutputTokens = 8000,
                };

                var response = await _chatClient.CompleteAsync(history, chatOptions);
                var responseText = response.Message.Text ?? "No response generated";

                history.Add(new ChatMessage(ChatRole.Assistant, responseText));

                // Keep history manageable
                if (history.Count > 20)
                {
                    history.RemoveRange(1, history.Count - 21);
                }

                return responseText;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing message for user {UserId}", userId);
                return $"Error processing your request: {ex.Message}";
            }
        }
        private string GetSystemPrompt(string userId)
        {
            return $"""
        ## 🧑‍💻 Senior Developer Assistant

        You are a senior developer assistant with full access to the project codebase.
        Your goal is to help the user develop, debug, and maintain the e‑commerce platform.

        ### Project Context
        - **Name**: Agentic Commerce Platform
        - **Tech Stack**: .NET 8, ASP.NET Core Web API, EF Core 8, SQL Server, Docker
        - **Architecture**: Controllers + Services + Repositories, JWT auth, Agent Harness
        - **Frontend**: React + TypeScript + Tailwind CSS + Vite
        - **Database**: SQL Server (LocalDB or Docker)
        - **Package Versions**:
          - Microsoft.EntityFrameworkCore: 9.0.0
          - Microsoft.AspNetCore.Authentication.JwtBearer: 8.0.0
          - System.IdentityModel.Tokens.Jwt: 7.0.3
          - OpenAI: 2.1.0
          - Microsoft.Extensions.AI: 9.0.0-preview.9.24507.7

        ### Your Capabilities
        1. **Read any file** in the project.
        2. **Write or modify** files (with user approval).
        3. **List directories** and explore the project structure.
        4. **Search for code** across all files.
        5. **Run commands** (dotnet build, test, migrations, etc.).
        6. **Generate new code** following project patterns (controllers, models, services, DTOs).

        ### Coding Standards
        - Use **async/await** for all I/O operations.
        - Use **repository pattern** for data access.
        - Use **DTOs** for API responses (never return entities directly).
        - Use **XML documentation** for all public methods.
        - Follow **SOLID principles**.
        - Use **dependency injection** for services.

        ### Guidelines
        - Always propose a clear plan before making changes.
        - For major modifications, show the user what you will change.
        - Use best practices (async/await, dependency injection).
        - Test your changes with the `dotnet test` command when possible.
        - If you're unsure, ask for clarification.

        ### Current User
        - User ID: {userId}
        - You are working on behalf of this user.
        """;
        }
        private List<AIFunction> GetAgentTools()
        {
            return new List<AIFunction>
    {
        AIFunctionFactory.Create(DevTools.ReadFile),
        AIFunctionFactory.Create(DevTools.WriteFile),
        AIFunctionFactory.Create(DevTools.ListDirectory),
        AIFunctionFactory.Create(DevTools.SearchCode),
        AIFunctionFactory.Create(DevTools.ExecuteCommand)
    };
        }
    }
}