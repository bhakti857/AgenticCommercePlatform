using AI_Ecommerce.Data;
using Microsoft.EntityFrameworkCore;

namespace AI_Ecommerce.Api.Services
{
    /// <summary>
    /// Long-running background service that deletes <c>ConversationHistory</c>
    /// rows older than a retention window, keeping the history table bounded.
    /// Runs once at startup, then once every 24 hours.
    /// </summary>
    public sealed class ConversationHistoryCleanupService : BackgroundService
    {
        private static readonly TimeSpan Retention = TimeSpan.FromDays(90);
        private static readonly TimeSpan Interval = TimeSpan.FromHours(24);

        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<ConversationHistoryCleanupService> _logger;

        public ConversationHistoryCleanupService(
            IServiceScopeFactory scopeFactory,
            ILogger<ConversationHistoryCleanupService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var timer = new PeriodicTimer(Interval);
            do
            {
                try
                {
                    await CleanupAsync(stoppingToken);
                }
                catch (Exception ex) when (ex is not OperationCanceledException)
                {
                    _logger.LogError(ex, "ConversationHistory cleanup run failed.");
                }
            } while (await timer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested);
        }

        private async Task CleanupAsync(CancellationToken ct)
        {
            var cutoff = DateTime.UtcNow.Subtract(Retention);
            await using var scope = _scopeFactory.CreateAsyncScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var deleted = await db.ConversationHistories
                .Where(c => c.CreatedAt < cutoff)
                .ExecuteDeleteAsync(ct);
            if (deleted > 0)
            {
                _logger.LogInformation(
                    "ConversationHistory cleanup: deleted {Count} rows older than {Cutoff}.",
                    deleted,
                    cutoff);
            }
        }
    }
}