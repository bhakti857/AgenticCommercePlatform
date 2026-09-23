using Microsoft.EntityFrameworkCore;

namespace AI_Ecommerce.Api.Services
{
    /// <summary>
    /// Backward-compatible pagination helper. Endpoints pass their full query
    /// plus optional <c>page</c>/<c>pageSize</c>. When both are present a wrapped
    /// envelope <c>{ items, page, pageSize, total, totalPages }</c> is returned;
    /// when omitted the endpoint returns a plain array, exactly as before — so
    /// existing UI/API consumers don't need to change.
    /// </summary>
    public static class PaginationHelper
    {
        private const int MaxPageSize = 200;

        public static async Task<object> ToResultAsync<T>(
            IQueryable<T> query,
            int? page,
            int? pageSize,
            CancellationToken ct = default)
        {
            if (!page.HasValue || !pageSize.HasValue)
                return await query.ToListAsync(ct);

            var safePage = Math.Max(1, page.Value);
            var safeSize = Math.Clamp(pageSize.Value, 1, MaxPageSize);

            var total = await query.CountAsync(ct);
            var items = await query
                .Skip((safePage - 1) * safeSize)
                .Take(safeSize)
                .ToListAsync(ct);

            return new PagedResult<T>
            {
                Items = items,
                Page = safePage,
                PageSize = safeSize,
                Total = total,
                TotalPages = total == 0 ? 0 : (int)Math.Ceiling(total / (double)safeSize)
            };
        }
    }

    /// <summary>Typed pagination envelope returned when <c>page</c>+<c>pageSize</c> are supplied.</summary>
    public sealed class PagedResult<T>
    {
        public List<T> Items { get; set; } = new();
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public int TotalPages { get; set; }
    }
}