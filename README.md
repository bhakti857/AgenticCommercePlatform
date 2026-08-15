# Agentic Commerce Platform

A full-stack e-commerce application with a built-in **AI development agent**.
The same agent "brain" is reachable from a terminal CLI, a JWT-protected Web
API, and a React chat UI — all three share one system prompt, one set of
tools, and one persisted conversation history in SQL Server.

## What this project actually is

Two things bolted together on purpose:

1. **A standard e-commerce backend** — users, products, orders, JWT auth,
   EF Core + SQL Server, a React/Vite storefront.
2. **An embedded coding agent** (`AgentHarness`) that can read/search the
   project's own source code, write files, and run shell commands
   (`dotnet build`, `git status`, migrations, etc.) on behalf of a developer,
   gated behind role checks and an approval step. It's essentially a
   lightweight, self-hosted "Copilot" scoped to this one repository,
   callable from the CLI, the API, or the web UI's `/agent` chat route.

## Architecture

```
AI-Ecommerce.Api ──┐
AI-Ecommerce.Cli ──┼──> AI-Ecommerce.Agent ──> AI-Ecommerce.Data ──> SQL Server
                    │         (Harness +            (EF Core models,
                    │          DevTools)              DbContext, migrations)
AI-Ecommerce.UI (React) ──HTTP/JWT──> AI-Ecommerce.Api
```

- **`AI-Ecommerce.Data`** — `ApplicationDbContext`, models (`User`, `Product`,
  `Order`, `OrderItem`, `ConversationHistory`), migrations, `DataSeeder`,
  password hashing (PBKDF2). A design-time `IDbContextFactory` supports
  `dotnet ef`.
- **`AI-Ecommerce.Agent`** — class library, no entry point.
  - `Harness/AgentHarness.cs`: builds the system prompt (project context +
    coding standards), registers tools, calls the `IChatClient`, retries on
    `tool_use_failed`, and loads/saves chat history per `SessionId` (capped
    at the most recent 20 messages). Takes an `allowWriteTools` flag so
    callers can restrict the agent to read-only tools.
  - `Harness/MockChatClient.cs`: no-op fallback used when no LLM API key is
    configured, so the app still runs end-to-end without a live model.
  - `Tools/DevTools.cs`: agent tools —
    `ReadFile` / `ListDirectory` / `SearchCode` (read-only, always allowed)
    and `WriteFile` / `ExecuteCommand` (gated behind a pluggable
    `ApprovalHandler`, and only registered for privileged callers — see
    Security below). `ExecuteCommand` picks `cmd.exe` or `/bin/sh -c`
    depending on the host OS. Resolves the project root by walking up to
    the nearest `.slnx`/`.sln`, so tool paths work no matter which project
    the host process was launched from.
- **`AI-Ecommerce.Cli`** — console entry point; wires an interactive y/n
  `ApprovalHandler` and talks to the agent via OpenRouter (`openrouter/free`),
  with full read/write tool access (it's an interactive developer tool).
- **`AI-Ecommerce.Api`** — ASP.NET Core Web API entry point.
  - Controllers: `AuthController` (register/login, JWT issuance),
    `ProductsController`, `OrdersController`, `AgentController`
    (`POST /api/agent/chat`, JWT-protected).
  - `IChatClient` wired to Groq (`llama-3.3-70b-versatile`) via
    `Microsoft.Extensions.AI`, falling back to the mock client if
    `GROQ_API_KEY` is unset.
  - Agent write/exec tools (`WriteFile`/`ExecuteCommand`) are only enabled
    for callers whose JWT `UserType` claim is MasterAdmin (`1`) or Admin
    (`2`) — everyone else gets read-only tools. The API's
    `ApprovalHandler` still auto-approves for those privileged callers
    (no interactive UI can gate concurrent HTTP requests yet — see
    `FutureScope.md`).
  - JWT signing secret is loaded from `.env`/`JWT_SECRET` (never hardcoded
    in `appsettings.json`) and validated at startup (fails fast if
    missing/too short).
  - CORS restricted to `http://localhost:5173`; seeds the database on
    startup (including a randomly-generated MasterAdmin password, printed
    once to the console).
- **`AI-Ecommerce.UI`** — React 19 + TypeScript + Tailwind + Vite frontend
  with `Auth`, `Products`, `Orders`, and `Agent` components/pages, calling
  the API over HTTP with a JWT.
- **`tests/AI-Ecommerce.Tests`** — test project (currently a stub, no real
  coverage yet — see `FutureScope.md`).

## Tech stack

| Layer | Technology |
|---|---|
| Runtime | .NET 8 (built/run with .NET SDK 10, backward compatible) |
| API | ASP.NET Core Web API, JWT bearer auth |
| Data | Entity Framework Core 9.0.0 (API/Agent/CLI), SQL Server (Docker or LocalDB) |
| AI | Microsoft.Extensions.AI 10.9.0 + `Microsoft.Extensions.AI.OpenAI` 10.8.0 over an OpenAI-compatible client (`OpenAI` SDK 2.12.0) |
| LLM providers | Groq (`llama-3.3-70b-versatile`) for the API; OpenRouter (`openrouter/free` auto-router) for the CLI — both free-tier, both rate-limited/flaky |
| Frontend | React 19, TypeScript, Tailwind CSS, Vite |
| Infra | Docker Compose (`sql-server`, `api`, `adminer`) |

> ⚠️ `AI-Ecommerce.Data.csproj` currently pins `Microsoft.EntityFrameworkCore.SqlServer`
> to **8.0.0** while every other project (API/Agent/CLI) is on **9.0.0** — this is
> the exact mismatch pattern that can cause a silent runtime
> `MissingMethodException`. It hasn't broken anything yet because NuGet's
> version resolution consolidates to the higher version, but it should be
> bumped to 9.0.0 explicitly to remove the risk. Tracked in `FutureScope.md`.

## Getting started

See **`HowtoRunProject.md`** for the full step-by-step guide (cloning,
pulling `.env` secrets from the private `my-secrets` repo, running each
project, and the git push/pull workflow). Quick version:

```bash
# 1. Clone this repo, then clone https://github.com/bhakti857/my-secrets.git
#    separately and copy its .env into this repo's root (or copy .env.example
#    and fill in real values).

# 2. Start SQL Server
docker-compose up -d sql-server

# 3. Apply migrations
cd src/AI-Ecommerce.Data
dotnet ef database update --startup-project ..\AI-Ecommerce.Cli

# 4a. Run the CLI agent
cd ../AI-Ecommerce.Cli
dotnet run

# 4b. ...or run the full stack (three terminals)
#     docker-compose up -d sql-server
#     dotnet run   (in AI-Ecommerce.Api, port 5015)
#     npm run dev  (in AI-Ecommerce.UI, port 5173)
```

Build the whole solution from the root (must target the `.slnx` explicitly,
since multiple project files exist there):

```bash
dotnet build AI-Ecommerce-Platform.slnx
```

## Security notes

- Never commit `.env` — secrets live in the separate `my-secrets` repo and
  are copied in locally (see `HowtoRunProject.md`).
- The agent's `WriteFile`/`ExecuteCommand` tools are role-gated on the API
  (MasterAdmin/Admin only) but still auto-approved for those roles — treat
  the API as trusted/local-only until a real pending-approval workflow
  exists.
- The seeded MasterAdmin password is randomly generated per environment and
  printed once to the console on first run — change it immediately after
  logging in.

## Known gaps

- Web API agent write/exec tools are role-gated but still auto-approved for
  admins — no real pending-approval UX yet.
- CLI and API always start a fresh chat `SessionId` — no "resume last
  conversation" feature.
- No automatic Groq → OpenRouter fallback; switching providers means editing
  `Program.cs` in both `AI-Ecommerce.Cli` and `AI-Ecommerce.Api`.
- `AI-Ecommerce.Data` pins EF Core 8.0.0 while other projects are on 9.0.0
  (see the tech stack table above).
- A few pre-existing nullable-reference warnings (`CS8604`, `CS8602`) in
  `AI-Ecommerce.Api` (`JwtService.cs`, `OrdersController.cs`).
- Test project has no real coverage yet.

See `FutureScope.md` for the full, prioritized list of bugs and next tasks,
and `AGENTS.md` for deeper contributor-facing notes (path-resolution rules,
provider quirks, approval-gating details, coding standards).
