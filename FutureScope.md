# Future Scope — Bugs, Risks & Next Tasks

Findings from a full scan of `AI-Ecommerce.Api`, `.Agent`, `.Cli`, `.Data`,
`.UI`, `tests/`, and Docker/config files. Grouped by severity/theme so the
next task can be picked straight off this list.

---

## 🔴 Critical bugs / security issues — ✅ RESOLVED

All five items below have been fixed and the solution rebuilds cleanly
(`dotnet build AI-Ecommerce-Platform.slnx` — 0 errors).

1. ~~**`DevTools.ExecuteCommand` hard-codes `cmd.exe`**~~ — **Fixed.**
   `ExecuteCommand` now checks `OperatingSystem.IsWindows()` and launches
   `cmd.exe /c` on Windows or `/bin/sh -c` on Linux (e.g. inside the API's
   Docker container).

2. ~~**Agent = remote code execution, auto-approved, on the Web API.**~~ —
   **Fixed.** `AgentHarness.ProcessMessageAsync` now takes an
   `allowWriteTools` flag and only registers `WriteFile`/`ExecuteCommand`
   when it's `true`. `AgentController.Chat` derives this from the caller's
   `UserType` JWT claim (only `1`/MasterAdmin and `2`/Admin get write/exec
   tools; everyone else gets read-only tools). The CLI still passes
   `allowWriteTools: true` since it's an interactive developer tool with its
   own y/n approval gate. (The API's auto-approve `ApprovalHandler` is still
   a known gap for admins themselves — see "Known/documented gaps" below —
   but the RCE surface is no longer reachable by arbitrary authenticated
   users.)

3. ~~**JWT signing secret committed in plaintext**~~ — **Fixed.**
   `appsettings.json`'s `Jwt:Secret` is now blank. `Program.cs` loads
   `.env` (via `DotNetEnv`) and overrides `Jwt:Secret` from a `JWT_SECRET`
   environment variable, then fails fast at startup (`throw
InvalidOperationException`) if the secret is missing or under 32
   characters. A random secret was generated and added to the local,
   git-ignored `.env`, and a new `.env.example` documents the required
   variable (including a PowerShell one-liner to generate a new secret).
   **Anyone who had access to the old committed secret should still treat
   it as compromised and confirm it isn't reused elsewhere.**

4. ~~**Seeded MasterAdmin has a hardcoded, publicly-known password**~~ —
   **Fixed.** `DataSeeder.SeedAsync` now generates a random password via
   `RandomNumberGenerator` on first run and prints it once to the console
   with a "change this immediately" warning, instead of hashing the
   constant `Admin@123`.

5. ~~**Order stock check-then-deduct is not transactional**~~ — **Fixed.**
   `OrdersController.CreateOrder` now runs inside a
   `_context.Database.BeginTransactionAsync()` block and deducts stock with
   an atomic conditional `UPDATE Products SET StockQuantity =
StockQuantity - @qty WHERE Id = @id AND StockQuantity >= @qty`
   (`ExecuteSqlInterpolatedAsync`), checking rows-affected before continuing.
   Concurrent requests for the last unit of a product can no longer both
   pass and oversell it; the transaction is rolled back on insufficient
   stock or any failure.

---

## 🟠 Functional gaps

6. **No conversation resume** — both CLI and API generate a brand-new
   `SessionId` (`Guid.NewGuid()`) on every process start / missing
   `SessionId` in the request. Users can't continue a previous chat unless
   the frontend explicitly persists and resends the same `SessionId`.

7. **No Groq ↔ OpenRouter fallback** — if Groq rate-limits (`429`) or
   OpenRouter's free model gets delisted (`404`), the whole chat request
   fails. There's no automatic retry against the other provider; switching
   requires manually editing `Program.cs` in two projects.

8. **`GetUserId()` in `OrdersController` can throw unhandled
   `FormatException`** if the JWT's `sub`/`NameIdentifier` claim is missing
   or malformed — results in a raw 500 instead of a clean 401/400.

9. **No global exception-handling middleware** — errors bubble up as raw
   ASP.NET 500 responses (stack traces in dev) instead of a consistent
   `ProblemDetails` JSON shape. Add `UseExceptionHandler` /
   a custom middleware.

10. **No pagination** on `ProductsController.GetAll` or
    `OrdersController.GetOrders` — fine for the seeded 3 products today,
    but will degrade as the catalog/order history grows.

11. **`ConversationHistory` grows unbounded in the database** — only the
    most recent 20 messages are _loaded_ per session, but old rows are
    never archived or deleted. Needs a cleanup/retention job.

12. **No refresh-token flow** — JWTs expire after 24h (`JwtService`) with no
    silent-renewal mechanism; users are simply logged out and must
    re-authenticate.

13. **`AuthController.Register` has no input validation** — no email format
    check, no password strength rules, `UserType` is client-supplied with
    a `4` (Customer) default but nothing stops a caller from registering as
    `UserType: 1` (MasterAdmin) directly through the API.

14. **Tests project is a stub** — `tests/AI-Ecommerce.Tests/UnitTest1.cs` is
    an empty placeholder test. No coverage exists for `AgentHarness`,
    `DevTools`, controllers, or the JWT/password flows.

---

## 🟡 Known/documented gaps (carried over from AGENTS.md, still open)

- CORS is hardcoded to `http://localhost:5173` only.
- `MockChatClient` doesn't simulate tool-calling at all, so agent tool
  behavior can only be tested against a real (rate-limited) LLM provider.
- Pre-existing nullable-reference warnings (`CS8604`, `CS8602`) in
  `JwtService.cs` and `OrdersController.cs`.

---

## 💡 Next-task ideas (feature/expansion backlog)

- **Shopping cart** as a first-class entity (currently orders are created
  directly from a client-supplied item list with no persisted cart/session).
- **Admin dashboard** in the React UI for managing products/orders/users
  (`UserType` role already models Admin/Employee/MasterAdmin, but there's no
  UI surface for it yet).
- **Product reviews/ratings**, inventory low-stock alerts, and order status
  webhooks/notifications.
- **Structured agent audit log** — record every `WriteFile`/`ExecuteCommand`
  the agent performs (who requested it, what ran, approved/denied) separate
  from the general chat `ConversationHistory`, for accountability.
- ~~**Rate limiting / throttling** on `AuthController` login and
  `AgentController` chat endpoints.~~ ✅ **Done.** `Program.cs` registers
  `AddRateLimiter` with two fixed-window policies: `"auth"` (5 requests/min,
  partitioned per client IP, applied to the whole `AuthController` so it
  covers both `/register` and `/login`) and `"agent-chat"` (10 requests/min,
  partitioned per authenticated user id, falling back to IP) applied to
  `AgentController.Chat`. Both reject over-limit requests immediately with
  `429` and a JSON body (`QueueLimit = 0`, no artificial delay). Verified
  live: 7 rapid `/api/auth/login` calls returned `401,401,401,401,401,429,429`.
- **Docker Compose parity** — add a `ui` service to `docker-compose.yml` so
  the React frontend can be brought up alongside `api`/`sql-server`/
  `adminer` with one command.

---

## Suggested immediate priority order

~~1. Rotate/secure the JWT secret (item 3).~~ ✅ Done.
~~2. Restrict or properly gate agent `WriteFile`/`ExecuteCommand` on the
API (item 2).~~ ✅ Done (role-gated).
~~3. Fix `ExecuteCommand`'s Windows-only `cmd.exe` call (item 1).~~ ✅ Done.
~~4. Wrap order creation in a transaction to prevent overselling (item 5).~~
✅ Done. 5. Add real test coverage for `AgentHarness` and the auth/order flows
(item 14) — **still open, recommended next task.**

### Follow-ups spawned by these fixes (not yet done)

- Build a real pending-approval workflow for the API's agent (currently
  admins are still auto-approved for write/exec — narrower blast radius now,
  but not a proper approval UX).
- Rotate the JWT secret that was previously committed to git history — the
  old value is compromised even though it's no longer in the working tree.
- Consider `UserType`-based authorization attributes/policies more broadly
  (currently only checked ad hoc in `AgentController`).
