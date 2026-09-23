using Microsoft.Extensions.AI;
using Microsoft.Extensions.Logging;

namespace AI_Ecommerce.Api.Services
{
    /// <summary>
    /// Routes chat completions to a primary LLM client, transparently falling
    /// back to a secondary client when the primary returns a retryable provider
    /// failure (HTTP 429 rate-limit or HTTP 404 model-not-available). Tool calls
    /// keep flowing through the outer <see cref="ChatClientBuilder"/> middleware,
    /// so the fallback is invisible to <c>AgentHarness</c>.
    /// </summary>
    public sealed class FallbackChatClient : DelegatingChatClient
    {
        private readonly IChatClient _fallback;
        private readonly ILogger<FallbackChatClient> _logger;

        public FallbackChatClient(IChatClient primary, IChatClient fallback, ILogger<FallbackChatClient> logger)
            : base(primary)
        {
            _fallback = fallback;
            _logger = logger;
        }

        public override async Task<ChatResponse> GetResponseAsync(
            IEnumerable<ChatMessage> chatMessages,
            ChatOptions? options = null,
            CancellationToken cancellationToken = default)
        {
            try
            {
                return await base.GetResponseAsync(chatMessages, options, cancellationToken);
            }
            catch (Exception ex) when (IsRetryable(ex))
            {
                _logger.LogWarning(
                    ex,
                    "Primary LLM provider failed ({ExceptionType}); falling back to secondary provider.",
                    ex.GetType().Name);
                return await _fallback.GetResponseAsync(chatMessages, options, cancellationToken);
            }
        }

        private static bool IsRetryable(Exception ex)
        {
            var message = ex.Message;
            return message.Contains("HTTP 429") || message.Contains("429 Too Many Requests")
                || message.Contains("rate_limit") || message.Contains("rate limit")
                || message.Contains("HTTP 404") || message.Contains("404 Not Found")
                || message.Contains("model_not_found") || message.Contains("Model Not Found");
        }
    }
}