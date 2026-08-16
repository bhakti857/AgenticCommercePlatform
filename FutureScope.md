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

## 🏗️ New architecture direction — Master/Transaction/Inventory/Accounting model

**Status: in progress.** The project is being re-architected around a
proper ERP-style schema with separate Customer and Employee logins, a
customer shopping flow, and an Employee dashboard for managing all master
data. This replaces the current single `Users`/`Products`/`Orders` tables.

### Phase 1 — Master tables (✅ done, this session)

Added under `src/AI-Ecommerce.Data/Models/Masters/` (kept **alongside** the
legacy `Users`/`Product`/`Order` tables — nothing removed yet, to avoid
breaking the running app mid-migration):

- `CustomerMaster` — customer login + profile (address/city/state/country/
  pincode/phone).
- `EmployeeMaster` — staff login, linked to `DepartmentMaster` +
  `UserTypeMaster`.
- `DepartmentMaster` — seeded: 1 CEO, 2 Software Developer.
- `UserTypeMaster` — seeded: 1 MasterAdmin, 2 Admin, 3 Senior, 4 Junior,
  5 User. (Distinct from the existing `UserTypes` table — that one stays
  tied to the legacy `Users.UserType` int column; this one is for
  `EmployeeMaster.UserTypeId`.)
- `EmployeeLogTable` / `CustomerLogTable` — per-login audit trail (IP, MAC,
  geolocation, OS/browser fingerprint, token).
- `CategoryMaster`, `SubCategoryMaster`, `UnitMaster`, `WarehouseMaster`,
  `VendorMaster`, `RawMaterialMaster`, `ProductMaster` (with 3-level
  approval fields: `Approval1By/At` … `Approval3By/At`).

Migration: `20260816134230_AddMasterTables`. Applied to the dev database —
verified via `sqlcmd` that all 12 new tables exist with seed data.

### Phase 2 — ✅ DONE (auth rework, full cutover)

Completed:

1. **Auth rework — full cutover.** `Users`/`UserTypes` tables are **dropped**.
   `AuthController` now operates entirely on `CustomerMaster` (public
   `/register`, `/login`) and `EmployeeMaster` (`/register-employee`,
   `/login`). `JwtService.GenerateToken` takes a `long accountId`, an
   `AccountType` claim (`"Customer"`/`"Employee"`), and (for employees only)
   a `UserTypeId` claim (1 MasterAdmin … 5 User — the `UserTypeMaster`
   numbering is now the *only* numbering scheme; the old 1-4 `Users.UserType`
   scheme is gone). A data migration
   (`20260816143630_Phase2AuthCutover.cs`) copied every existing `Users` row
   into `CustomerMasters` (old UserType 4) or `EmployeeMasters` (old
   UserType 1/2/3, mapped to new UserTypeId 1/2/5 respectively, Department
   defaulted to Software Developer) before dropping the legacy tables.
   Verified live: `bhaktiraut.goldmedal@gmail.com` migrated correctly and
   logs in as a Customer with the new claim shape.
2. **Privilege-escalation bug fixed** — `register-employee` now requires the
   caller to be Employee + UserTypeId 1 or 2, **and** enforces
   `request.UserTypeId >= callerUserTypeId` — an Admin (2) can no longer
   mint a MasterAdmin (1) account; only a MasterAdmin can create another
   MasterAdmin.
3. **UserType numbering conflict resolved** — there is now exactly one
   scheme (`UserTypeMaster`: 1 MasterAdmin, 2 Admin, 3 Senior, 4 Junior,
   5 User), used consistently by `AuthController`, `AgentController`, and
   `EmployeeRegister.tsx`.
4. **Agent restricted to employees only** — `AgentController.Chat` now
   checks the `AccountType` claim and returns 403 Forbid for any Customer
   JWT before doing anything else. Write/exec tool access remains gated to
   UserTypeId 1/2 within the employee population. Verified live: a Customer
   JWT gets 403 from `/api/agent/chat`.
5. **`Order.CustomerId`** changed from `Guid` (FK to legacy `Users.Id`) to
   `long` (FK to `CustomerMasters.CustomerId`); `OrdersController.GetUserId()`
   now parses a `long` and rejects non-Customer JWTs. Pre-existing `Orders`
   rows (1 row, dev/test data) were cleared as part of the migration since
   the old Guid customer id can't be losslessly mapped to the new bigint
   key — acceptable for dev data, would need a real backfill strategy for a
   production cutover.

Not part of this pass (still open, see Phase 2 remainder below):

1. **Transaction tables** — `SalesOrder`, `SalesOrderItem`, `PurchaseOrder`,
   `PurchaseOrderItem`, `Payment`, `Receipt`, `CreditNote` (+`CreditNoteItem`),
   `DebitNote` (+`DebitNoteItem`).
2. **Inventory tables** — `ProductStock`, `RawMaterialStock`,
   `StockTransaction`, `StockTransfer`, `StockAdjustment`.
3. **Accounting tables** — `Ledger`, `LedgerEntry`.
4. **Customer app** — login, product browse, profile (address fields already
   on `CustomerMaster`), cart, checkout with COD/UPI *selection only* (no
   real payment processing — explicitly out of scope per requirements),
   order placement, order tracking.
5. **Employee dashboard** — landing dashboard + Add/List (grid + Edit/Delete)
   page pair for every master table (Product, Category, SubCategory, Unit,
   Warehouse, Customer, Vendor, RawMaterial, Department, UserType, etc.).
6. **Per-page SQL documentation** — for every page/feature built above,
   create a `.sql` file (e.g. `docs/sql/ProductMaster.sql`) listing the
   tables it reads/writes, for onboarding/audit purposes.
7. **Login audit logging** — `EmployeeLogTable`/`CustomerLogTable` exist in
   the schema but nothing writes to them yet; wiring this up (IP, device
   fingerprint, etc. on every successful login) is still pending.

---

## 🆕 New bugs/risks found (post Phase-1 master-tables scan)

Findings from re-scanning the repo after the `AddMasterTables` migration,
`register-employee` endpoint, and UI redesign landed. Grouped by severity;
none of these are fixed yet.

### High

1. **✅ FIXED (Phase 2).** Privilege escalation in
   `POST /api/auth/register-employee` — now enforces
   `request.UserTypeId >= callerUserTypeId`, so an Admin (2) can no longer
   mint a MasterAdmin (1) account; only a MasterAdmin can create another
   MasterAdmin.

2. **Agent write/exec still auto-approved for privileged users** —
   `src/AI-Ecommerce.Api/Program.cs` (~line 159) has an explicit
   `// TODO: replace with a proper "pending approval" workflow` above
   `DevTools.ApprovalHandler = async (description) => { ... return true; }`.
   Combined with `AgentController`'s role gate (`UserType == 1 || 2` →
   `allowWriteTools = true`), this means **any stolen Master/Master Admin
   JWT is enough to have the agent execute arbitrary shell commands or
   overwrite files on the API host with zero human confirmation.** This was
   flagged before as a "known gap" but is worth re-surfacing now that a
   second privileged role (`Master`, UserType 2) exists via
   `register-employee` — the blast radius of a compromised token just grew.

### Medium

3. **✅ FIXED (Phase 2).** The legacy `UserTypes` table and `Users.UserType`
   scheme are gone entirely (dropped in `20260816143630_Phase2AuthCutover`).
   `UserTypeMaster` (1 MasterAdmin, 2 Admin, 3 Senior, 4 Junior, 5 User) is
   now the single authoritative numbering, used consistently across
   `AuthController`, `AgentController`, and `EmployeeRegister.tsx`.

4. **✅ RESOLVED (Phase 2).** `CustomerMaster`/`EmployeeMaster` are no
   longer orphaned — `AuthController` and `OrdersController` now read/write
   them directly. (`ProductMaster`/`CategoryMaster`/etc. remain unused
   pending the transaction/inventory-table phases.)

5. **`docker-compose.yml` still has a hardcoded SQL `SA_PASSWORD`** —
   `SA_PASSWORD: 'YourStrong!Passw0rd'` is committed in plaintext. Lower
   severity than the JWT secret fix (this is a local dev-only container,
   not internet-exposed by default), but it's inconsistent with the
   env-var-based secret handling now used for `JWT_SECRET` — should
   probably come from `.env` too for consistency, especially since
   `HowtoRunProject.md` already documents an env-var-driven setup.

6. **EF Core version mismatch still unresolved** — confirmed still present:
   `AI-Ecommerce.Data.csproj` pins `Microsoft.EntityFrameworkCore.SqlServer`
   `8.0.0` while `.Api`/`.Cli` pin `9.0.0` and `.Agent` pins
   `Microsoft.EntityFrameworkCore` `9.0.0`. Previously documented as a
   known gap in README/AGENTS.md — repeating here because the new
   `Masters` models/migration were added without fixing it, so the risk is
   still live and now touches more entities.

### Low

7. **No navigation link to `/employeeregister`** —
   `src/AI-Ecommerce.UI/src/components/Layout/Header.tsx`'s `navLinks`
   array doesn't include the employee-registration page. It's reachable
   only by typing the URL directly; a Master Admin/Master using the UI has
   no discoverable way to find it.

8. **Client-side-only authorization check in `EmployeeRegister.tsx`** —
   the "Access restricted" gate is `user?.userType === 1 || user?.userType
   === 2`, which is a UX nicety only (the real enforcement is server-side
   in `register-employee`, confirmed correct in isolation — see finding 1
   for the actual server-side gap). Worth a code comment clarifying this is
   not a security boundary, to avoid future confusion.

9. **Test suite has no coverage for anything added this session** —
   `tests/AI-Ecommerce.Tests/UnitTest1.cs` is still the default placeholder
   test. No tests exist for `register-employee`'s authorization logic, the
   new Master entities, `RateLimiting` policies, or the agent's
   `allowWriteTools` gating — all high-value, security-relevant behavior
   that's currently only verified by ad hoc manual `curl`/PowerShell checks
   during development.

---



~~1. Rotate/secure the JWT secret (item 3).~~ ✅ Done.
~~2. Restrict or properly gate agent `WriteFile`/`ExecuteCommand` on the
API (item 2).~~ ✅ Done (role-gated).
~~3. Fix `ExecuteCommand`'s Windows-only `cmd.exe` call (item 1).~~ ✅ Done.
~~4. Wrap order creation in a transaction to prevent overselling (item 5).~~
✅ Done. 5. Add real test coverage for `AgentHarness` and the auth/order flows
(item 14) — **still open, recommended next task.**
6. Continue the new architecture: Phase 2 items above (auth rework is the
   natural next step since everything else — customer app, employee
   dashboard, agent restriction — depends on knowing which table/claims a
   logged-in user belongs to).
7. **Fix the `register-employee` privilege-escalation bug (new finding #1
   above)** — a Master account can currently mint a Master Admin account.
   Quick fix, high impact, should happen before Phase 2 auth rework builds
   further on top of it.
8. Resolve the dual `UserType`/`UserTypeMaster` numbering conflict (new
   finding #3) before writing any more Phase 2 code that depends on role
   numbers.

### Follow-ups spawned by these fixes (not yet done)

- Build a real pending-approval workflow for the API's agent (currently
  admins are still auto-approved for write/exec — narrower blast radius now,
  but not a proper approval UX).
- Rotate the JWT secret that was previously committed to git history — the
  old value is compromised even though it's no longer in the working tree.
- Consider `UserType`-based authorization attributes/policies more broadly
  (currently only checked ad hoc in `AgentController`).

