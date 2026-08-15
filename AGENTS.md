# AGENTS.md — Agentic Commerce Platform

This file gives AI coding assistants (Claude, Cursor, Copilot, or the project's
own CLI/API agent) the context needed to work on this repo correctly, without
re-discovering the same gotchas every session.

---

## 1. Project Overview

**Agentic Commerce Platform** is a full-stack e-commerce app with an AI
development agent built in. It has three ways to talk to the same agent brain:

- **CLI** (`AI-Ecommerce.Cli`) — terminal chat, for developer use
- **Web API** (`AI-Ecommerce.Api`) — `POST /api/agent/chat`, JWT-protected
- **React UI** (`AI-Ecommerce.UI`, `/agent` route) — browser chat

All three route through the same `AgentHarness` class, which owns the system
prompt, tool registration, retry logic, and (as of this session) SQL Server
persistence of conversation history.

---

## 2. Tech Stack & Exact Versions (verified working, Aug 2026)

| Package | Version | Notes |
|---|---|---|
| .NET SDK | 10.0.301 (installed) | Targets `net8.0` — SDK is backward compatible, this is fine |
| Microsoft.Extensions.AI | 10.9.0 | Stable release. Preview `9.0.0-preview.*` is obsolete — do not reintroduce |
| Microsoft.Extensions.AI.Abstractions | 10.9.0 | Must match `Microsoft.Extensions.AI` exactly |
| Microsoft.Extensions.AI.OpenAI | 10.8.0 | Provides `AsIChatClient()` off `ChatClient`, not `OpenAIClient` directly |
| OpenAI (SDK) | 2.12.0 | Must be ≥2.12.0 to satisfy `Microsoft.Extensions.AI.OpenAI 10.8.0`'s transitive requirement |
| Microsoft.EntityFrameworkCore.* | 9.0.0 | **All EF Core packages across ALL projects must be on the same major version.** Mixing EF Core 8 and 9 assemblies causes a runtime `MissingMethodException` on `TypeMappingInfo` — no compile error, only fails at first DB access |
| Microsoft.EntityFrameworkCore.Design | 9.0.0 | Must be on the **startup project** (`AI-Ecommerce.Cli` or `.Api`) for `dotnet ef` commands to work at all |

**Rule: when touching any `Microsoft.Extensions.AI*` or `Microsoft.EntityFrameworkCore*`
package in one `.csproj`, check all other `.csproj` files in the solution for the
same package and align versions.** This bit us multiple times — NuGet's `NU1605`
"package downgrade" error is your friend here; don't suppress it, fix the actual
mismatch it's pointing at.

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
DEEPSEEK_API_KEY=
GITHUB_TOKEN=
GROQ_API_KEY=
OPENROUTER_API_KEY=
CONNECTION_STRING=Server=localhost,1433;Database=AgenticCommerceDB;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;
```

- `CONNECTION_STRING` is read by the **CLI** at runtime (`Program.cs`) and by
  the **EF Core design-time factory** (`ApplicationDbContextFactory.cs`, used
  by `dotnet ef` commands).
- The **Web API** does *not* read `.env` for its connection string — it reads
  `ConnectionStrings:DefaultConnection` from `appsettings.json` /
  `appsettings.Development.json`. Keep these pointed at the same database
  (Docker SQL Server) as `.env`'s `CONNECTION_STRING`, or the API and CLI will
  silently use two different databases with two different sets of data.

---

## 5. LLM Provider — Groq / OpenRouter, with known flakiness

The agent uses free-tier hosted LLMs via OpenAI-compatible endpoints. **Both
providers have real limitations; expect to swap between them.**

### Groq (`https://api.groq.com/openai/v1`)
- Model used: `llama-3.3-70b-versatile`
- Free tier TPM (tokens/minute) limits are easy to hit during active
  development/testing — expect `HTTP 429 rate_limit_exceeded`.
- `llama-3.1-8b-instant` has an even *lower* TPM cap than `70b-versatile` —
  don't switch to it thinking it'll help with rate limits, it's worse for that.

### OpenRouter (`https://openrouter.ai/api/v1`)
- Free-tier model availability **rotates without notice** — a specific model ID
  like `meta-llama/llama-3.3-70b-instruct:free` can be delisted from the free
  tier overnight, returning `HTTP 404` with a message pointing at the paid slug.
- **Use `openrouter/free` (the auto-router)** instead of pinning to a specific
  model ID. It always routes to *some* currently-available free model, so the
  code doesn't break every time OpenRouter's free lineup changes.
- Before pinning to a specific free model ID for quality/consistency reasons,
  verify it's still free at `openrouter.ai/models` (filter: Price = Free) —
  don't trust any hardcoded list, including this one, without checking.

### Tool-calling reliability (both providers)
- Llama models occasionally either:
  1. Fail to produce valid structured tool-call JSON → Groq/OpenRouter reject
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
- **CLI**: `Program.cs` sets `ApprovalHandler` to an interactive
  `Console.ReadLine()` y/n prompt. This blocks correctly in a single-user
  terminal context.
- **Web API**: `Program.cs` sets `ApprovalHandler` to **auto-approve
  everything** (`return true`), because a console y/n prompt can't work
  across concurrent HTTP requests from multiple users, and there's no
  interactive approval UI built yet. **This is a known gap, not a finished
  feature** — the API will execute `WriteFile`/`ExecuteCommand` unattended.
  Do not expose this API beyond trusted/local use until a real
  pending-approval workflow (e.g. return a confirmation token, require a
  follow-up call to execute) is built.

---

## 7. Conversation Persistence

- `ConversationHistory` (in `AI-Ecommerce.Data/Models/`) stores every message
  (`system`, `user`, `assistant`) with `SessionId`, `UserId`, `Content`,
  `CreatedAt`.
- `AgentHarness.LoadHistoryAsync` loads by `SessionId` on each call; if none
  exist yet for that session, it seeds the system prompt as the first row.
- History is capped to the most recent 20 messages when loaded, to keep
  context size manageable.
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
# 4. Run the CLI or API
cd ..\AI-Ecommerce.Cli
dotnet run
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

---

## 10. Solution Structure Quick Reference

```
AgenticCommercePlatform/
├── src/
│   ├── AI-Ecommerce.Api/       # ASP.NET Core Web API (JWT auth, controllers)
│   ├── AI-Ecommerce.Agent/     # AgentHarness + DevTools (class library, no Main)
│   ├── AI-Ecommerce.Cli/       # Console entry point — dotnet run works here
│   ├── AI-Ecommerce.Data/      # EF Core models, ApplicationDbContext, migrations
│   └── AI-Ecommerce.UI/        # React + TypeScript + Tailwind + Vite frontend
├── tests/AI-Ecommerce.Tests/
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

---

## 11. Known Gaps / Not Yet Built

- Web API approval gating is auto-approve-only (see section 6) — no real
  pending-approval UX yet.
- CLI and API sessions always start a fresh `SessionId` — no "resume last
  conversation" feature yet.
- No automatic Groq → OpenRouter fallback — switching providers currently
  requires manually editing `Program.cs` in both CLI and API projects.
- Several pre-existing nullable-reference warnings (`CS8604`, `CS8602`) in
  `AI-Ecommerce.Api` (`JwtService.cs`, `OrdersController.cs`) — harmless,
  not yet cleaned up.
