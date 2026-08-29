# Future Scope — Bugs, Risks & Next Tasks

Findings from a full scan of `AI-Ecommerce.Api`, `.Agent`, `.Cli`, `.Data`,
`.UI`, `tests/`, and Docker/config files. Grouped by severity/theme so the
next task can be picked straight off this list.

---

## 🔎 Diagnostics scan — 2026-08-17

### Confirmed diagnostics

1. **Moderate vulnerable dependency (`NU1902`)** —
   `System.IdentityModel.Tokens.Jwt` **7.0.3** in
   `src/AI-Ecommerce.Api/AI-Ecommerce.Api.csproj` is reported by NuGet as
   affected by [GHSA-59j7-ghrg-fj52](https://github.com/advisories/GHSA-59j7-ghrg-fj52).
   Upgrade to a patched compatible release and retest JWT authentication.

2. **Four nullable-reference warnings (`CS8604`)** — the solution builds, but
   these code paths rely on values established elsewhere rather than expressing
   their non-null contract locally:
   - `Services/JwtService.cs:28` — `Jwt:Secret` passed to
     `Encoding.UTF8.GetBytes`.
   - `Program.cs:67` — the same configuration value passed to
     `Encoding.UTF8.GetBytes` for JWT validation.
   - `Controller/OrdersController.cs:38` and `:66` — `OrderItems` used as the
     source for `Select` while the navigation collection is nullable.
   Startup currently validates the JWT secret, so the first two are not known
   runtime failures; remove the warnings by making that validated contract
   explicit. Initialise `OrderItems` or make its non-null invariant explicit.

3. **React Fast Refresh lint warning** —
   `src/AI-Ecommerce.UI/src/contexts/AuthContext.tsx:52` triggers
   `react(only-export-components)` because it exports both `AuthProvider` and
   `useAuth`. Move the hook/context to a separate module or suppress the rule
   deliberately if the current arrangement is preferred.

4. **Debug build file-lock failure while API is running** — a regular Debug
   build fails with `MSB3021`/`MSB3027` because `AI-Ecommerce.Api` (PID 28500
   during this scan) locks its copied `AI-Ecommerce.Data.dll` and
   `AI-Ecommerce.Agent.dll`. This is a development-workflow diagnostic, not a
   compilation error: an isolated-output build succeeded with **0 errors**.
   Stop the API before an in-place build, or use an isolated output path.

5. **Test suite is effectively untested** — `dotnet test` passes **1/1** test,
   but it is the empty default `UnitTest1.Test1`. This confirms the test runner
   works, not application behaviour; the authentication, order, and agent
   permission paths still need real automated coverage.

### Validation results

- `.NET`: isolated `dotnet build AI-Ecommerce-Platform.slnx --no-restore`
  succeeded with **0 errors, 5 warnings** (the vulnerability plus the four
  nullable warnings above).
- UI: `npm run build` succeeded.
- UI lint: completed with the one Fast Refresh warning above.
- Tests: `dotnet test` succeeded, **1 passed**.

---

## 🟠 Functional gaps

1. **No conversation resume** — both CLI and API generate a brand-new
   `SessionId` (`Guid.NewGuid()`) on every process start / missing
   `SessionId` in the request. Users can't continue a previous chat unless
   the frontend explicitly persists and resends the same `SessionId`.

2. **No Groq ↔ OpenRouter fallback** — if Groq rate-limits (`429`) or
   OpenRouter's free model gets delisted (`404`), the whole chat request
   fails. There's no automatic retry against the other provider; switching
   requires manually editing `Program.cs` in two projects.

3. **`GetUserId()` in `OrdersController` can throw unhandled
   `FormatException`** if the JWT's `sub`/`NameIdentifier` claim is missing
   or malformed — results in a raw 500 instead of a clean 401/400.

4. **No global exception-handling middleware** — errors bubble up as raw
   ASP.NET 500 responses (stack traces in dev) instead of a consistent
   `ProblemDetails` JSON shape. Add `UseExceptionHandler` /
   a custom middleware.

5. **No pagination** on `ProductsController.GetAll` or
    `OrdersController.GetOrders` — fine for the seeded 3 products today,
    but will degrade as the catalog/order history grows.

6. **`ConversationHistory` grows unbounded in the database** — only the
    most recent 20 messages are _loaded_ per session, but old rows are
    never archived or deleted. Needs a cleanup/retention job.

7. **No refresh-token flow** — JWTs expire after 24h (`JwtService`) with no
    silent-renewal mechanism; users are simply logged out and must
    re-authenticate.

8. **`AuthController.Register` has no input validation** — no email format
    check or password strength rules. Public registration always creates a
    customer.

9. **Tests project is a stub** — `tests/AI-Ecommerce.Tests/UnitTest1.cs` is
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
2. **Agent restricted to employees only** — `AgentController.Chat` now
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
6. **Per-page SQL documentation** — ✅ DONE (2026-08-19). Added `docs/sql/`
   with one `.sql` file per page (`.sql` only, no `.md`): existing pages
   (`01-LoginPage` … `06-AgentChatPage`) document the actual queries the
   current code runs; planned pages (`07-CustomerProfilePage` …
   `20-EmployeeMasterPage`) document the intended CRUD + audit-column
   statements for the not-yet-built customer profile, order tracking,
   dashboard, and employee master pages. Each file lists the tables it
   reads/writes for onboarding/audit purposes.
7. **Login audit logging** — `EmployeeLogTable`/`CustomerLogTable` exist in
   the schema but nothing writes to them yet; wiring this up (IP, device
   fingerprint, etc. on every successful login) is still pending.

---

## 🆕 New bugs/risks found (post Phase-1 master-tables scan)

Findings from re-scanning the repo after the `AddMasterTables` migration,
`register-employee` endpoint, and UI redesign landed. Grouped by severity;
only unresolved items are retained below.

### High

1. **Agent write/exec still auto-approved for privileged users** —
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

1. **`docker-compose.yml` still has a hardcoded SQL `SA_PASSWORD`** —
   `SA_PASSWORD: 'YourStrong!Passw0rd'` is committed in plaintext. Lower
   severity than the JWT secret fix (this is a local dev-only container,
   not internet-exposed by default), but it's inconsistent with the
   env-var-based secret handling now used for `JWT_SECRET` — should
   probably come from `.env` too for consistency, especially since
   `HowtoRunProject.md` already documents an env-var-driven setup.

### Low

1. **No navigation link to `/employeeregister`** —
   `src/AI-Ecommerce.UI/src/components/Layout/Header.tsx`'s `navLinks`
   array doesn't include the employee-registration page. It's reachable
   only by typing the URL directly; a Master Admin/Master using the UI has
   no discoverable way to find it.

2. **Client-side-only authorization check in `EmployeeRegister.tsx`** —
   the "Access restricted" gate is `user?.userType === 1 || user?.userType
   === 2`, which is a UX nicety only (the real enforcement is server-side
   in `register-employee`, confirmed correct in isolation). Worth a code comment clarifying this is
   not a security boundary, to avoid future confusion.

3. **Test suite has no coverage for anything added this session** —
   `tests/AI-Ecommerce.Tests/UnitTest1.cs` is still the default placeholder
   test. No tests exist for `register-employee`'s authorization logic, the
   new Master entities, `RateLimiting` policies, or the agent's
   `allowWriteTools` gating — all high-value, security-relevant behavior
   that's currently only verified by ad hoc manual `curl`/PowerShell checks
   during development.

---

## Recommended next actions

1. Add meaningful test coverage for the agent, authentication, and order
   flows.
2. Continue the remaining transaction, inventory, accounting, customer-app,
   and employee-dashboard work in Phase 2.

### Remaining security follow-ups

- Build a real pending-approval workflow for the API's agent (currently
  admins are still auto-approved for write/exec — narrower blast radius now,
  but not a proper approval UX).
- Rotate the JWT secret that was previously committed to git history — the
  old value is compromised even though it's no longer in the working tree.
- Consider `UserType`-based authorization attributes/policies more broadly
  (currently only checked ad hoc in `AgentController`).

