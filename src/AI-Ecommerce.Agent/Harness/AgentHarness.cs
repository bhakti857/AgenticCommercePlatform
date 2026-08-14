using AI_Ecommerce.Agent.Tools;
using AI_Ecommerce.Data;
using AI_Ecommerce.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Logging;

namespace AI_Ecommerce.Agent.Harness
{
    public class AgentHarness
    {
        private readonly IChatClient _chatClient;
        private readonly ILogger<AgentHarness> _logger;
        private readonly ApplicationDbContext _dbContext;

        public AgentHarness(IChatClient chatClient, ILogger<AgentHarness> logger, ApplicationDbContext dbContext)
        {
            _chatClient = chatClient;
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<string> ProcessMessageAsync(string userId, string message, string sessionId)
        {
            try
            {
                var history = await LoadHistoryAsync(userId, sessionId);

                history.Add(new ChatMessage(ChatRole.User, message));
                await SaveMessageAsync(sessionId, userId, "user", message);

                var chatOptions = new ChatOptions
                {
                    Tools = GetAgentTools().Cast<AITool>().ToList(),
                    MaxOutputTokens = 1024,
                };

                ChatResponse response;
                var maxRetries = 2;
                var attempt = 0;

                while (true)
                {
                    try
                    {
                        response = await _chatClient.GetResponseAsync(history, chatOptions);
                        break;
                    }
                    catch (Exception ex) when (attempt < maxRetries && ex.Message.Contains("tool_use_failed"))
                    {
                        attempt++;
                        _logger.LogWarning("Tool call failed, retrying ({Attempt}/{Max})...", attempt, maxRetries);
                        await Task.Delay(500);
                    }
                }

                var responseText = response.Text ?? "No response generated";

                await SaveMessageAsync(sessionId, userId, "assistant", responseText);

                return responseText;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing message for user {UserId}", userId);
                return $"Error processing your request: {ex.Message}";
            }
        }

        private async Task<List<ChatMessage>> LoadHistoryAsync(string userId, string sessionId)
        {
            var records = await _dbContext.ConversationHistories
                .Where(c => c.SessionId == sessionId)
                .OrderBy(c => c.CreatedAt)
                .ToListAsync();

            if (records.Count == 0)
            {
                var systemPrompt = GetSystemPrompt(userId);
                await SaveMessageAsync(sessionId, userId, "system", systemPrompt);
                return new List<ChatMessage> { new ChatMessage(ChatRole.System, systemPrompt) };
            }

            // Keep history manageable — only load the most recent 20 messages
            var recent = records.Count > 20
                ? records.Skip(records.Count - 20).ToList()
                : records;

            return recent.Select(r => new ChatMessage(MapRole(r.Role), r.Content)).ToList();
        }

        private async Task SaveMessageAsync(string sessionId, string userId, string role, string content)
        {
            _dbContext.ConversationHistories.Add(new ConversationHistory
            {
                SessionId = sessionId,
                UserId = userId,
                Role = role,
                Content = content,
                CreatedAt = DateTime.UtcNow
            });

            await _dbContext.SaveChangesAsync();
        }

        private static ChatRole MapRole(string role) => role switch
        {
            "system" => ChatRole.System,
            "user" => ChatRole.User,
            "assistant" => ChatRole.Assistant,
            _ => ChatRole.User
        };

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
          - OpenAI: 2.12.0
          - Microsoft.Extensions.AI: 10.9.0

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