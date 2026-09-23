# AGENTS.md — Agentic Commerce Platform

This file gives AI coding assistants (Claude, Cursor, Copilot, or the project's
own CLI/API agent) the context needed to work on this repo correctly, without
re-discovering the same gotchas every session.

---

## 1. Project Overview

**Agentic Commerce Platform** is a full-stack commerce app (master /
transaction / inventory / accounting / cart schema) with an AI development
agent built in. It has three ways to talk to the same agent brain:

- **CLI** (`AI-Ecommerce.Cli`) — terminal chat, for developer use
- **Web API** (`AI-Ecommerce.Api`) — `POST /api/agent/chat`, JWT-protected
- **React UI** (`AI-Ecommerce.UI`, `/agent` route) — browser chat

All three route through the same `AgentHarness` class, which owns the system
prompt, tool registration, retry logic, and SQL Server persistence of
conversation history (`ConversationHistory`).

---

## 2. Tech Stack & Exact Versions (verified working, Sep 2026)

| Package | Version | Notes |
|---|---|---|
| .NET SDK | 10.0.301 (installed) | Targets `net8.0` — SDK is backward compatible, this is fine |
| Microsoft.Extensions.AI | 10.9.0 | Stable release. Preview `9.0.0-preview.*` is obsolete — do not reintroduce |
| Microsoft.Extensions.AI.Abstractions | 10.9.0 | Must match `Microsoft.Extensions.AI` exactly |
| Microsoft.Extensions.AI.OpenAI | 10.8.0 | Provides `AsIChatClient()` off `ChatClient`, not `OpenAIClient` directly |
| OpenAI (SDK) | 2.12.0 | Must be ≥2.12.0 to satisfy `Microsoft.Extensions.AI.OpenAI 10.8.0`'s transitive requirement |
| Microsoft.EntityFrameworkCore.* | 9.0.0 | **All EF Core packages across ALL projects are now aligned on 9.0.0** — this was the source of the historical `MissingMethodException` on `TypeMappingInfo` (no compile error, only fails at first DB access). Keep it this way |
| Microsoft.EntityFrameworkCore.Design | 9.0.0 | Must be on the **startup project** (`AI-Ecommerce.Cli` or `.Api`) for `dotnet ef` commands to work at all |
| System.IdentityModel.Tokens.Jwt | 8.23.0 | Upgraded from 7.0.3 to clear `NU1902` (GHSA-59j7-ghrg-fj52 / CVE-2024-21319). Never drop below 7.1.2 |
| Microsoft.AspNetCore.Authentication.JwtBearer | 8.0.0 | Ships with ASP.NET Core 8; unified against the IdentityModel 8.x line via the direct `System.IdentityModel.Tokens.Jwt` reference |

**Rule: when touching any `Microsoft.Extensions.AI*`, `Microsoft.EntityFrameworkCore*`,
or `Microsoft.IdentityModel*`/`System.IdentityModel.Tokens.Jwt`
package in one `.csproj`, check all other `.csproj` files in the solution for the
same package and align versions.** This bit us multiple times — NuGet's `NU1605`
"package downgrade" error is your friend here; don't suppress it, fix the actual
mismatch it's pointing at. (The old README claim that `AI-Ecommerce.Data` was
pinned to 8.0.0 is stale — it has been 9.0.0 for a while.)

---

## 3. Path Resolution Rules

- `DevTools.cs` resolves the project root by **walking up parent directories
  from `Directory.GetCurrentDirectory()` until it finds a `.slnx` or `.sln`
  file** (see `FindProjectRoot()`). This makes all tool file paths (`ReadFile`,
  `WriteFile`, `ListDirectory`, `SearchCode`, `ExecuteCommand`) resolve
  correctly regardless of which subproject the process was launched from.
- **Do not** replace this with `Directory.GetCurrentDirectory()` directly — that
  resolves to wherever `dotnet run` was invoked from (e.g.
  `src/AI-Ecommerce.Cli`), not the solution root, and silently breaks every
  tool path.
- `.env` is loaded with a **relative path from each project's own folder**:
  - CLI (`src/AI-Ecommerce.Cli/Program.cs`): `Env.Load("../../.env")`
  - API (`src/AI-Ecommerce.Api/Program.cs`): `Env.Load("../../.env")`
  - Both resolve up to the solution root, where the single shared `.env` lives.
- `.env` is git-ignored (confirmed via `git check-ignore -v .env`). It is
  **never committed**. `.env.example` documents required keys with empty values.

---

## 4. Secrets (`.env` — never commit, template below)

```
# Core API keys
DEEPSEEK_API_KEY=
GITHUB_TOKEN=
GROQ_API_KEY=
OPENROUTER_API_KEY=

# SQL Server — read by CLI + EF Core design-time factory
CONNECTION_STRING=Server=localhost,1433;Database=AgenticCommerceDB;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;

# CLI chat provider: "opencode" (default) or "openrouter"
LLM_PROVIDER=opencode
OPENCODE_URL=http://127.0.0.1:4096
OPENCODE_SERVER_PASSWORD=

# API chat model on Groq (optional override, default openai/gpt-oss-20b)
GROQ_MODEL=openai/gpt-oss-20b

# JWT signing key (32+ chars), read by the Web API
JWT_SECRET=
```

- `CONNECTION_STRING` is read by the **CLI** at runtime (`Program.cs`) and by
  the **EF Core design-time factory** (`ApplicationDbContextFactory.cs`, used
  by `dotnet ef` commands).
- The **Web API** does *not* read `.env` for its connection string — it reads
  `ConnectionStrings:DefaultConnection` from `appsettings.json` /
  `appsettings.Development.json`. Keep these pointed at the same database
  (Docker SQL Server) as `.env`'s `CONNECTION_STRING`, or the API and CLI will
  silently use two different databases with two different sets of data.
- The API reads `JWT_SECRET` from the environment (`.env`) and injects it over
  `Jwt:Secret`; startup throws if it's missing or shorter than 32 chars.

---

## 5. LLM Providers — Groq / OpenRouter / opencode, with known flakiness

The agent uses hosted LLMs via OpenAI-compatible endpoints. **All providers
have real limitations; expect to swap between them.**

### CLI chat provider selection (`LLM_PROVIDER`)

The **CLI** (`src/AI-Ecommerce.Cli`) can talk to either of two backends,
selected at startup by the `LLM_PROVIDER` env var:

- `opencode` (default) — talks to a running `opencode serve` HTTP server via
  `OpenCodeClient` (`src/AI-Ecommerce.Cli/OpenCode/`). The opencode server
  runs the full opencode agent (its own tools + model routing through the
  `opencode`/Zen provider). It is **not** OpenAI-compatible, so it installs as
  a separate loop in `Program.RunOpenCodeChatAsync`, bypassing `IChatClient`
  and `AgentHarness`. History is maintained by the opencode server per session.
  Requires `opencode serve --port 4096` running in another terminal (customize
  with `OPENCODE_URL`, default `http://127.0.0.1:4096`, optionally protected
  with `OPENCODE_SERVER_PASSWORD`).
- `openrouter` — the original path through `IChatClient` → `AgentHarness`
  (SQL persistence via `ConversationHistory`), with an interactive y/n
  `DevTools.ApprovalHandler`.

For the `opencode` provider:
- **Chat history is maintained across CLI restarts**: the active opencode
  session id is persisted to `.opencode-session` (next to the CLI output
  assembly, i.e. `bin/Debug/net8.0/.opencode-session`) and resumed on the
  next `dotnet run`. Delete that file to start a brand-new conversation.

### Groq (`https://api.groq.com/openai/v1`) — used by the Web API
- Model: **`GROQ_MODEL` env var**, default `openai/gpt-oss-20b` (don't hardcode
  a model id — it's configurable for a reason). Older notes referenced
  `llama-3.3-70b-versatile`; that is only available today by setting
  `GROQ_MODEL` explicitly — verify it still exists on Groq before pinning it.
- Free tier TPM (tokens/minute) limits are easy to hit during active
  development/testing — expect `HTTP 429 rate_limit_exceeded`.
- `llama-3.1-8b-instant` has an even *lower* TPM cap than models like `70b` —
  don't switch to it thinking it'll help with rate limits, it's worse for that.
- The Web API now falls back to OpenRouter automatically: if `OPENROUTER_API_KEY`
  is set, `FallbackChatClient` (`AI-Ecommerce.Api/Services/FallbackChatClient.cs`)
  wraps the Groq client and retries the request on OpenRouter whenever Groq
  returns HTTP 429 (rate-limit) or HTTP 404 (model unavailable). The outer
  `ChatClientBuilder.UseFunctionInvocation()` middleware sits *outside* the
  fallback, so tool calling keeps working identically on either provider.

### OpenRouter (`https://openrouter.ai/api/v1`) — CLI `openrouter` path
- Free-tier model availability **rotates without notice** — a specific model ID
  like `meta-llama/llama-3.3-70b-instruct:free` can be delisted from the free
  tier overnight, returning `HTTP 404` with a message pointing at the paid slug.
- **Use `openrouter/free` (the auto-router)** instead of pinning to a specific
  model ID. It always routes to *some* currently-available free model, so the
  code doesn't break every time OpenRouter's free lineup changes.
- Before pinning to a specific free model ID for quality/consistency reasons,
  verify it's still free at `openrouter.ai/models` (filter: Price = Free) —
  don't trust any hardcoded list, including this one, without checking.

### Tool-calling reliability (all GPT-compatible providers)
- Some models occasionally either:
  1. Fail to produce valid structured tool-call JSON → the provider rejects
     with `HTTP 400 tool_use_failed`. **Not a bug in this codebase** — retry
     usually succeeds. `AgentHarness.ProcessMessageAsync` already retries up to
     2 times on this specific error.
  2. Write out a fake `<function=ToolName>{...}</function>` as plain text
     instead of a real structured tool call. This happened when
     `ChatOptions.ToolMode` was left unset — **do not** "fix" it by setting
     `ToolMode = ChatToolMode.RequireAny`, because that forces a tool call on
     *every* turn, including the final summary turn after a tool result comes
     back, causing an infinite double-approval loop in the CLI. Leave
     `ToolMode` unset (defaults to `Auto`) and rely on the retry logic instead.

---

## 6. Approval Gating for Writes/Commands

`DevTools.WriteFile` and `DevTools.ExecuteCommand` check a static hook before
running:

```csharp
public static Func<string, Task<bool>>? ApprovalHandler { get; set; }
```

- `ReadFile`, `ListDirectory`, `SearchCode` are read-only and **never** gated.
- **CLI (`LLM_PROVIDER=openrouter`)**: `Program.cs` sets `ApprovalHandler` to
  an interactive `Console.ReadLine()` y/n prompt. This blocks correctly in a
  single-user terminal context. (The default `opencode` path doesn't use
  `DevTools` at all — the opencode server manages its own approvals.)
- **Web API**: `Program.cs` sets `ApprovalHandler` to register the operation in
  an in-memory `ApprovalGate` (singleton) and await an explicit decision —
  the API does **not** auto-approve:
  - `GET /api/agent/approvals` — list pending operations (employees only).
  - `POST /api/agent/approvals/{token}` — `{ "Approved": true|false }`;
    restricted by the `MasterAdminOrAdmin` policy (`UserTypeId` 1 or 2).
  - Unresolved approvals **auto-deny after 10 minutes**, so a conversation
    cannot hang forever; the tool then reports "cancelled" back to the model.
  - The chat request that triggered the write stays in-flight until the decision
    lands. The React UI now has an in-chat approval panel (`Chat.tsx`) that polls
    `GET /api/agent/approvals` every 5 seconds and offers Approve/Deny buttons
    (calls `POST /api/agent/approvals/{token}`); the token body field is
    `{ "approved": true|false }` (camelCase binding is accepted).
  - Write tools are still only *registered* for JWT `UserTypeId` 1/2, and
    `/api/agent/chat` rejects customers — layers on top of the gate.

---

## 7. Conversation Persistence

- `ConversationHistory` (in `AI-Ecommerce.Data/Models/`) stores every message
  (`system`, `user`, `assistant`) with `SessionId`, `UserId`, `Content`,
  `CreatedAt`.
- `AgentHarness.LoadHistoryAsync` loads by `SessionId` on each call; if none
  exist yet for that session, it seeds the system prompt as the first row.
- History is capped to the most recent 20 messages when loaded, to keep
  context size manageable.
- The API's `AgentController.Chat` now accepts a `SessionId` from the client
  and returns it in the response, so a browser can continue a conversation
  across turns. The React UI (`Chat.tsx`) persists the `SessionId` to
  `localStorage` (`agentSessionId`), so reloading the browser resumes the same
  conversation; a "Start a new conversation" button clears it. The CLI
  `openrouter` path and the `opencode` path both resume across restarts
  (SQL history / `.opencode-session` respectively).
- **Refresh tokens**: `POST /api/auth/login` and `POST /api/auth/register` now
  also return a long-lived `RefreshToken` (opaque, 64 random bytes, stored only
  as a SHA-256 hash in the new `RefreshTokens` table). `POST /api/auth/refresh`
  exchanges a still-valid refresh token for a fresh JWT and rotates it (old row
  revoked, new one issued), and `POST /api/auth/revoke` invalidates it at logout.
  The UI's `api/client.ts` silently refreshes a 401'd request once before giving
  up, so users don't get logged out mid-session at the 24h JWT boundary. Any new
  consumer must persist the refresh token and reuse it via `/auth/refresh`.
- **Migrations require a design-time factory** because `Program.cs` uses
  top-level statements with a manually-built `ServiceProvider`, which the EF
  Core CLI tools can't introspect. See `ApplicationDbContextFactory.cs`
  (implements `IDesignTimeDbContextFactory<ApplicationDbContext>`) — this is
  required infrastructure, not optional boilerplate.
- To add a new migration after changing any entity:
  ```bash
  cd src/AI-Ecommerce.Data
  dotnet ef migrations add <Name> --startup-project ..\AI-Ecommerce.Cli
  dotnet ef database update --startup-project ..\AI-Ecommerce.Cli
  ```

### Seed data pipeline (important)

- Migrations create schema **only** (`DepartmentMaster` and `UserTypeMaster`
  are seeded via EF `HasData`). All other reference data lives in
  `schema/data.xlsx` and is imported by `scripts/import-from-excel.ps1`
  (exported back by `scripts/export-data.ps1`, which regenerates
  `schema/data.xlsx`, `schema/schema.txt`, and `scripts/seed-data.sql`).
- `DataSeeder.SeedAsync` (called on API startup) only *checks presence* and
  prints a warning listing empty tables — it **does not insert data**. The old
  behavior of creating a random MasterAdmin password is gone; staff accounts
  are created via `POST /api/auth/register-employee` or the `/employeeregister`
  UI by an existing MasterAdmin/Admin.

---

## 8. Local Environment Setup (new machine checklist)

Secrets and database contents do **not** travel via git. After `git pull` on a
new machine:

```bash
# 1. Recreate .env manually (see template in section 4) — never committed
# 2. Start the database container
docker-compose up -d sql-server
# 3. Apply migrations to the fresh container
cd src/AI-Ecommerce.Data
dotnet ef database update --startup-project ..\AI-Ecommerce.Cli
# 4. Import Excel seed data (all tables)
cd ..\..
$env:SQL_SA_PASSWORD = 'YourStrong!Passw0rd'
.\scripts\import-from-excel.ps1
# 5. Run the CLI or API
cd ..\AI-Ecommerce.Cli
dotnet run          # CLI: needs `opencode serve --port 4096` running (or LLM_PROVIDER=openrouter)
```

Full stack (CLI/API + React UI) requires **three processes running
simultaneously** in separate terminals:
1. `docker-compose up -d sql-server` (background, start once)
2. `dotnet run` in `AI-Ecommerce.Api` (must stay running — port 5015)
3. `npm run dev` in `AI-Ecommerce.UI` (must stay running — port 5173)

CORS is currently hardcoded to allow only `http://localhost:5173` in
`AI-Ecommerce.Api/Program.cs`. If Vite serves on a different port, update the
CORS policy origin to match, or the browser will silently block all API
requests.

---

## 9. Coding Standards (project conventions)

- async/await for all I/O
- Repository pattern for data access
- DTOs for API responses — never return EF entities directly
- XML doc comments on public methods
- SOLID principles
- Dependency injection for services
- `[ApiController]` + explicit route templates; `[Authorize]` where required
- Master entities use soft-delete via the global `DeletedAt == null` query
  filter and `AuditableEntity` audit columns (`CreatedBy/ModifiedBy/DeletedBy`)

---

## 10. Solution Structure Quick Reference

```
AgenticCommercePlatform/
├── src/
│   ├── AI-Ecommerce.Api/       # ASP.NET Core Web API (JWT auth, controllers,
│   │                           #   rate limiting, login audit)
│   ├── AI-Ecommerce.Agent/     # AgentHarness + DevTools (class library, no Main)
│   ├── AI-Ecommerce.Cli/       # Console entry point — dotnet run works here
│   ├── AI-Ecommerce.Data/      # EF Core models, ApplicationDbContext, migrations
│   └── AI-Ecommerce.UI/        # React + TypeScript + Tailwind + Vite frontend
│       └── src/                # App.tsx, api/client.ts, contexts, components/
├── tests/AI-Ecommerce.Tests/   # xUnit (empty stub — see FutureScope.md)
├── scripts/                    # import-from-excel.ps1, export-data.ps1, seed-data.sql
├── schema/                     # data.xlsx (source of truth), schema.txt (DDL)
├── docker-compose.yml          # sql-server, api, adminer services
└── AI-Ecommerce-Platform.slnx  # solution file — build/restore/clean must target this explicitly
                                 #   when run from the solution root (multiple project files present)
```

**`AI-Ecommerce.Agent` is a class library — `dotnet run` does not work there.**
Run from `AI-Ecommerce.Cli` or `AI-Ecommerce.Api` instead.

**From the solution root, always target the `.slnx` explicitly:**
```bash
dotnet build AI-Ecommerce-Platform.slnx
```
Running bare `dotnet build` from the root fails with `MSB1011` (ambiguous —
multiple project/solution files present).

A stale duplicate `src/components/` folder at the repo root (unreferenced by any
project) was removed as cleanup — do not recreate it; the real UI components
live under `AI-Ecommerce.UI/src/components/`.

---

## 11. Known Gaps / Not Yet Built

- Conversation history rows older than 90 days are deleted daily by
  `ConversationHistoryCleanupService` (a hosted `BackgroundService` in the
  API) — bounded growth per configurable window, one `ExecuteDeleteAsync` a day.
- Pagination on list endpoints (`/api/products`, `/api/orders`, `/api/catalog`,
  `/api/sales-orders`, `/api/audit/*-logs`) is **backward-compatible optional**:
  pass `?page=1&pageSize=50` and get `{ items, page, pageSize, total, totalPages }`;
  omit them and you still get the plain array. Don't change existing UI calls
  that don't page.
- `ProductMaster` 3-step approval (`Approval1At/2At/3At`) exists in schema and
  the dashboard lists pending approvals, but there is no approve/deny action in
  the API or UI yet.
- Purchase orders, credit/debit notes, receipts, and inventory transfers exist
  as schema + models only — no controllers or UI.
- The test project has no real coverage.