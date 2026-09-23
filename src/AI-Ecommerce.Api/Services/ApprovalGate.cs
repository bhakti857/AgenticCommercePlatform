using System.Collections.Concurrent;

namespace AI_Ecommerce.Api.Services
{
    /// <summary>
    /// In-memory store of pending agent operations (WriteFile / ExecuteCommand)
    /// that are awaiting an explicit Approve/Deny decision before the agent may
    /// proceed. Each pending operation gets a random token; the operator presents
    /// that token to <c>POST /api/agent/approvals/{token}</c> to resolve it. The
    /// awaiting conversation is unblocked once the decision lands (or times out).
    /// </summary>
    public sealed class ApprovalGate
    {
        private sealed class Entry
        {
            public required string Token { get; init; }
            public required string Description { get; init; }
            public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
            public DateTime? ResolvedAt { get; set; }
            public bool? Decision { get; set; }
            public TaskCompletionSource<bool> Tcs { get; } =
                new(TaskCreationOptions.RunContinuationsAsynchronously);
        }

        private readonly ConcurrentDictionary<string, Entry> _entries = new();

        /// <summary>Registers a new pending operation and returns its approval token.</summary>
        public string Submit(string description)
        {
            var token = Guid.NewGuid().ToString("N");
            _entries[token] = new Entry { Token = token, Description = description };
            return token;
        }

        /// <summary>
        /// Awaits the decision for <paramref name="token"/> — true = approved,
        /// false = denied or unknown token. Callers should race this against a
        /// timeout so an abandoned approval cannot hang a conversation forever.
        /// </summary>
        public Task<bool> WaitForDecisionAsync(string token)
        {
            if (!_entries.TryGetValue(token, out var entry))
                return Task.FromResult(false);
            return entry.Tcs.Task;
        }

        /// <summary>Resolves a pending operation. Returns false if the token is unknown.</summary>
        public bool Decide(string token, bool approved)
        {
            if (!_entries.TryGetValue(token, out var entry))
                return false;
            entry.Decision = approved;
            entry.ResolvedAt = DateTime.UtcNow;
            entry.Tcs.TrySetResult(approved);
            return true;
        }

        /// <summary>Snapshot of unresolved operations, oldest first.</summary>
        public IReadOnlyList<PendingApprovalInfo> ListPending()
        {
            return _entries.Values
                .Where(e => e.ResolvedAt == null)
                .OrderBy(e => e.CreatedAt)
                .Select(e => new PendingApprovalInfo(e.Token, e.Description, e.CreatedAt))
                .ToList();
        }
    }

    /// <summary>Immutable description of a pending approval, returned by the API.</summary>
    public sealed record PendingApprovalInfo(string Token, string Description, DateTime CreatedAt);
}