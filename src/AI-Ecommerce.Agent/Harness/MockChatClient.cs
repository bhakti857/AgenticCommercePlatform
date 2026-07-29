using Microsoft.Extensions.AI;
using System.Runtime.CompilerServices;

namespace AI_Ecommerce.Agent.Harness
{
    public class MockChatClient : IChatClient
    {
        private readonly ChatClientMetadata _metadata = new("MockChatClient", new Uri("https://mock"), "Mock");

        public ChatClientMetadata Metadata => _metadata;

        public Task<ChatCompletion> CompleteAsync(
            IList<ChatMessage> messages,
            ChatOptions? options = null,
            CancellationToken cancellationToken = default)
        {
            var completion = GenerateCompletion(messages);
            return Task.FromResult(completion);
        }

        public async IAsyncEnumerable<StreamingChatCompletionUpdate> CompleteStreamingAsync(
            IList<ChatMessage> messages,
            ChatOptions? options = null,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var completion = GenerateCompletion(messages);
            yield return new StreamingChatCompletionUpdate
            {
                Role = ChatRole.Assistant,
                Text = completion.Message.Text
            };
            await Task.CompletedTask;
        }

        public TService? GetService<TService>(object? serviceKey = null) where TService : class
        {
            return null;
        }

        public object? GetService(Type serviceType, object? serviceKey = null)
        {
            return null;
        }

        public void Dispose()
        {
        }

        private ChatCompletion GenerateCompletion(IList<ChatMessage> messages)
        {
            var lastMessage = messages.LastOrDefault(m => m.Role == ChatRole.User);
            var userMessage = lastMessage?.Text ?? "Hello!";

            string responseText = userMessage.ToLower() switch
            {
                var msg when msg.Contains("running shoes") =>
                    "We have several running shoes available. Check out our 'Running Shoes Pro' for $129.99 or 'Comfort Walk' for $89.99.",
                var msg when msg.Contains("recommend") || msg.Contains("suggest") =>
                    "Based on popular items, I'd recommend our Wireless Headphones ($89.99) or Smart Watch ($199.99).",
                var msg when msg.Contains("cart") || msg.Contains("add") =>
                    "I can help you add items to your cart. Just tell me what you'd like to buy and the quantity.",
                var msg when msg.Contains("help") || msg.Contains("what") =>
                    "I'm your shopping assistant! You can ask me about products, get recommendations, or manage your cart.",
                _ => $"I received your message: '{userMessage}'. I'm your shopping assistant. How can I help you today?"
            };

            var chatMessage = new ChatMessage(ChatRole.Assistant, responseText);
            return new ChatCompletion([chatMessage]);
        }
    }
}