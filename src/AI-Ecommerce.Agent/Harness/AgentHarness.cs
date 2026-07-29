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

                // Call the new CompleteAsync method
                var response = await _chatClient.CompleteAsync(history);
                var responseText = response.Message.Text ?? "No response generated";

                history.Add(new ChatMessage(ChatRole.Assistant, responseText));

                // Keep history manageable (last 20 messages)
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
                ## Enterprise E-Commerce Assistant

                You are an AI assistant for a large e-commerce platform.

                ### Your Capabilities
                1. **Product Search**: Help users find products using natural language
                2. **Personalized Recommendations**: Suggest products based on user preferences
                3. **Order Management**: Assist with placing, tracking, and managing orders
                4. **Cart Management**: Add, view, and remove items from cart
                5. **Customer Support**: Answer questions about products, shipping, returns

                ### User Context
                - Current User ID: {userId}

                ### Rules
                1. Always verify product availability before recommending
                2. For orders over $1000, request human approval
                3. Respect user privacy - don't share personal information
                4. Be professional, helpful, and concise
                """;
        }
    }
}