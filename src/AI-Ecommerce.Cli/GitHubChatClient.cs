using Microsoft.Extensions.AI;
using OpenAI;
using OpenAI.Chat;
using System.ClientModel;
using System.Runtime.CompilerServices;

namespace AI_Ecommerce.Cli
{
    public class GitHubChatClient : IChatClient
    {
        private readonly OpenAI.Chat.ChatClient _chatClient;

        public GitHubChatClient(string token, string model = "gpt-4o-mini")
        {
            var credential = new ApiKeyCredential(token);
            var options = new OpenAIClientOptions
            {
                Endpoint = new Uri("https://models.inference.ai.azure.com")
            };
            var client = new OpenAIClient(credential, options);
            _chatClient = client.GetChatClient(model);
        }

        public ChatClientMetadata Metadata { get; } = new ChatClientMetadata(
            "GitHub Models",
            new Uri("https://github.com/marketplace/models"),
            "GitHub");

        public async Task<Microsoft.Extensions.AI.ChatCompletion> CompleteAsync(
            IList<Microsoft.Extensions.AI.ChatMessage> messages,
            Microsoft.Extensions.AI.ChatOptions? options = null,
            CancellationToken cancellationToken = default)
        {
            var openAiMessages = new List<OpenAI.Chat.ChatMessage>();
            foreach (var m in messages)
            {
                if (m.Role == Microsoft.Extensions.AI.ChatRole.System)
                    openAiMessages.Add(OpenAI.Chat.ChatMessage.CreateSystemMessage(m.Text));
                else if (m.Role == Microsoft.Extensions.AI.ChatRole.User)
                    openAiMessages.Add(OpenAI.Chat.ChatMessage.CreateUserMessage(m.Text));
                else if (m.Role == Microsoft.Extensions.AI.ChatRole.Assistant)
                    openAiMessages.Add(OpenAI.Chat.ChatMessage.CreateAssistantMessage(m.Text));
                else
                    openAiMessages.Add(OpenAI.Chat.ChatMessage.CreateUserMessage(m.Text));
            }

            var requestOptions = new OpenAI.Chat.ChatCompletionOptions
            {
                MaxOutputTokenCount = options?.MaxOutputTokens ?? 4000,
                Temperature = options?.Temperature ?? 0.7f,
                FrequencyPenalty = options?.FrequencyPenalty ?? 0,
                PresencePenalty = options?.PresencePenalty ?? 0,
            };
            if (options?.TopP != null) requestOptions.TopP = options.TopP.Value;

            var response = await _chatClient.CompleteChatAsync(openAiMessages, requestOptions, cancellationToken);
            // ✅ EXPLICIT CAST to the OpenAI type
            var completion = (OpenAI.Chat.ChatCompletion)response.Value;
            var messageText = completion.Choices[0].Message.Content[0].Text;

            return new Microsoft.Extensions.AI.ChatCompletion(
                new Microsoft.Extensions.AI.ChatMessage(Microsoft.Extensions.AI.ChatRole.Assistant, messageText));
        }

        public async IAsyncEnumerable<Microsoft.Extensions.AI.StreamingChatCompletionUpdate> CompleteStreamingAsync(
            IList<Microsoft.Extensions.AI.ChatMessage> messages,
            Microsoft.Extensions.AI.ChatOptions? options = null,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var openAiMessages = new List<OpenAI.Chat.ChatMessage>();
            foreach (var m in messages)
            {
                if (m.Role == Microsoft.Extensions.AI.ChatRole.System)
                    openAiMessages.Add(OpenAI.Chat.ChatMessage.CreateSystemMessage(m.Text));
                else if (m.Role == Microsoft.Extensions.AI.ChatRole.User)
                    openAiMessages.Add(OpenAI.Chat.ChatMessage.CreateUserMessage(m.Text));
                else if (m.Role == Microsoft.Extensions.AI.ChatRole.Assistant)
                    openAiMessages.Add(OpenAI.Chat.ChatMessage.CreateAssistantMessage(m.Text));
                else
                    openAiMessages.Add(OpenAI.Chat.ChatMessage.CreateUserMessage(m.Text));
            }

            var requestOptions = new OpenAI.Chat.ChatCompletionOptions
            {
                MaxOutputTokenCount = options?.MaxOutputTokens ?? 4000,
                Temperature = options?.Temperature ?? 0.7f,
                FrequencyPenalty = options?.FrequencyPenalty ?? 0,
                PresencePenalty = options?.PresencePenalty ?? 0,
            };
            if (options?.TopP != null) requestOptions.TopP = options.TopP.Value;

            var updates = _chatClient.CompleteChatStreamingAsync(openAiMessages, requestOptions, cancellationToken);

            await foreach (var update in updates)
            {
                if (update.ContentUpdate.Count > 0)
                {
                    var text = string.Join("", update.ContentUpdate.Select(c => c.Text));
                    yield return new Microsoft.Extensions.AI.StreamingChatCompletionUpdate
                    {
                        Role = Microsoft.Extensions.AI.ChatRole.Assistant,
                        Text = text
                    };
                }
            }
        }

        public object? GetService(Type serviceType, object? serviceKey = null) => null;
        public TService? GetService<TService>(object? serviceKey = null) where TService : class => null;
        public void Dispose() { }
    }
}