# How to Run This Project

This guide covers everything needed to go from `git clone` to a fully
running stack (database + API + CLI + UI), including where the secrets come
from and how to push/pull changes correctly.

---

## 1. Prerequisites

Install these before you start:

- [.NET SDK](https://dotnet.microsoft.com/download) 8.0+ (SDK 10.x is fine —
  it's backward compatible, the projects target `net8.0`)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) (for SQL
  Server via `docker-compose`)
- [Node.js](https://nodejs.org/) 18+ and npm (for the React UI)
- [Git](https://git-scm.com/)
- `dotnet-ef` CLI tool (for database migrations):
  ```bash
  dotnet tool install --global dotnet-ef
  ```

---

## 2. Clone the project

```bash
git clone https://github.com/bhakti857/AgenticCommercePlatform.git
cd AgenticCommercePlatform
```

---

## 3. Get the `.env` secrets

`.env` is **never committed** to this repo (it's git-ignored) — it lives in
a separate private secrets repo instead:

```bash
git clone https://github.com/bhakti857/my-secrets.git
```

From that cloned `my-secrets` repo, copy the `.env` file for this project
into the solution root of `AgenticCommercePlatform` (the same folder as
`AgenticCommercePlatform.slnx`):

```bash
# Windows PowerShell (adjust paths as needed)
Copy-Item ..\my-secrets\AgenticCommercePlatform\.env .\.env

# macOS/Linux
cp ../my-secrets/AgenticCommercePlatform/.env ./.env
```

If you don't have access to `my-secrets`, or need to set up a fresh `.env`
from scratch, copy the template and fill in real values instead:

```bash
Copy-Item .env.example .env
```

`.env` must contain:

```
DEEPSEEK_API_KEY=
GITHUB_TOKEN=
GROQ_API_KEY=
OPENROUTER_API_KEY=
CONNECTION_STRING=Server=localhost,1433;Database=AgenticCommerceDB;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;
JWT_SECRET=
```

- `GROQ_API_KEY` — used by the Web API's chat agent (Groq, `llama-3.3-70b-versatile`)
- `OPENROUTER_API_KEY` — used by the CLI's chat agent (OpenRouter, `openrouter/free`)
- `CONNECTION_STRING` — used by the CLI and by EF Core design-time tools (`dotnet ef`)
- `JWT_SECRET` — signs auth tokens issued by the Web API; must be 32+
  random characters. Generate one with:
  ```powershell
  [Convert]::ToBase64String((1..48 | ForEach-Object { Get-Random -Maximum 256 }))
  ```

> ⚠️ Never commit `.env` to `AgenticCommercePlatform`. Any secret updates
> should be made in the `my-secrets` repo and re-copied locally, not added
> to this repo's history.

---

## 4. Start the database

```bash
docker-compose up -d sql-server
```

This starts SQL Server 2022 in a container on port `1433` (credentials match
the `CONNECTION_STRING` above: user `sa`, password `YourStrong!Passw0rd`).
`adminer` (a web DB browser at `http://localhost:8080`) is also available
via `docker-compose up -d adminer` if you want to inspect tables visually.

---

## 5. Apply database migrations

```bash
cd src/AI-Ecommerce.Data
dotnet ef database update --startup-project ..\AI-Ecommerce.Cli
cd ../..
```

This creates the `AgenticCommerceDB` schema. The app also seeds a
MasterAdmin user and a few sample products automatically on first run (see
console output for the generated admin password — it's only printed once).

---

## 6. Run the project

You have three ways to interact with the same AI agent + e-commerce
backend. Pick what you need:

### Option A — CLI only (fastest way to try the AI agent)

```bash
cd src/AI-Ecommerce.Cli
dotnet run
```

Type messages at the `🤖 >` prompt; type `exit` to quit. Uses
`OPENROUTER_API_KEY` from `.env` (falls back to a mock responder if unset).

### Option B — Full stack (API + React UI)

Requires **three terminals running at the same time**:

```bash
# Terminal 1 — database (background, start once)
docker-compose up -d sql-server

# Terminal 2 — Web API (must stay running — http://localhost:5015)
cd src/AI-Ecommerce.Api
dotnet run

# Terminal 3 — React UI (must stay running — http://localhost:5173)
cd src/AI-Ecommerce.UI
npm install
npm run dev
```

Open `http://localhost:5173` in your browser. Register a user, log in, and
try the `/agent` chat route. The API uses `GROQ_API_KEY` from `.env` (falls
back to a mock responder if unset).

### Option C — Everything via Docker Compose

```bash
docker-compose up -d --build
```

This brings up `sql-server`, `api` (port `5000`/`5001`), and `adminer`
(port `8080`) together. Run the React UI separately with `npm run dev` in
`src/AI-Ecommerce.UI` — it isn't in `docker-compose.yml` yet.

---

## 7. Build / test from the command line

Always target the `.slnx` explicitly from the solution root (there are
multiple project files, so bare `dotnet build` fails with `MSB1011`):

```bash
dotnet build AI-Ecommerce-Platform.slnx
dotnet test AI-Ecommerce-Platform.slnx
```

---

## 8. Git workflow — pushing and pulling code

This repo uses a standard feature-branch + PR workflow against `main`.

### Pulling the latest changes

```bash
git checkout main
git pull origin main
```

If you're on a feature branch and want to bring in the latest `main`:

```bash
git checkout your-branch-name
git pull origin main --rebase
```

### Making and pushing changes

```bash
# 1. Create a branch for your change
git checkout -b your-feature-name

# 2. Make your changes, then stage and commit
git add .
git commit -m "Describe what you changed and why"

# 3. Push your branch to GitHub
git push origin your-feature-name
```

Then open a Pull Request on GitHub (`bhakti857/AgenticCommercePlatform`)
targeting `main`, and merge once reviewed.

If you're pushing directly to `main` (small/solo changes):

```bash
git add .
git commit -m "Describe what you changed and why"
git push origin main
```

### Before every push — sanity checklist

- [ ] `.env` is **not** staged (`git status` should never show it — it's
      git-ignored, but double-check if you ever force-add files)
- [ ] `dotnet build AI-Ecommerce-Platform.slnx` succeeds
- [ ] Any new/changed entity needs a matching EF Core migration (see below)
- [ ] Secrets/API keys only ever go in `.env` or the `my-secrets` repo —
      never in `appsettings.json`, source files, or commit messages

### After changing a database model

```bash
cd src/AI-Ecommerce.Data
dotnet ef migrations add <DescriptiveName> --startup-project ..\AI-Ecommerce.Cli
dotnet ef database update --startup-project ..\AI-Ecommerce.Cli
cd ../..
git add src/AI-Ecommerce.Data/Migrations
git commit -m "Add migration: <DescriptiveName>"
```

---

## 9. Troubleshooting

| Symptom | Fix |
|---|---|
| `dotnet build` fails with `MSB1011` | You ran `dotnet build` without a target from the solution root — use `dotnet build AI-Ecommerce-Platform.slnx` |
| API throws `Jwt:Secret is missing or too short` on startup | Your `.env` doesn't have a valid `JWT_SECRET` (32+ chars) — see step 3 |
| `dotnet ef` commands fail / can't find DbContext | Always pass `--startup-project ..\AI-Ecommerce.Cli` (or `.Api`) — the CLI has the design-time factory `dotnet ef` needs |
| Browser can't reach the API from the React UI | Confirm the API is running on port 5015/5000 and CORS in `Program.cs` allows `http://localhost:5173` (the Vite dev server's default port) |
| "using mock client" printed in console | The relevant API key (`GROQ_API_KEY` for the API, `OPENROUTER_API_KEY` for the CLI) isn't set in `.env` — the app still runs, just with canned responses instead of a real LLM |
| SQL Server container won't start / port conflict | Make sure nothing else is using port `1433`, then `docker-compose down` and `docker-compose up -d sql-server` again |

See `AGENTS.md` for deeper architecture/contributor notes, and
`FutureScope.md` for known gaps and planned improvements.
