# Future Scope — Requirements & Backlog

Current state (Sep 2026): Master/Transaction/Inventory/Accounting/Cart schema
is in place; auth is cut over to `CustomerMaster`/`EmployeeMaster`; Excel
(`schema/data.xlsx`) is the single source of truth for seed data; the agent is
restricted to employees; cart + checkout (COD/UPI), sales-order tracking, the
employee dashboard, master-data CRUD, login audit, and rate limiting are all
implemented. The legacy `Product`/`Order` tables remain for backward
compatibility.

Status legend: **Open** = not started · **Partial** = some capability exists ·
**Done** = shipped.

---

## 🔒 Security

| # | Task | Priority | Status |
|---|------|----------|--------|
| 1 | Build a real pending-approval workflow for agent WriteFile/ExecuteCommand (currently auto-approved in the API) | High | Open |
| 2 | Rotate JWT secret that was previously committed to git history | High | Open |
| 3 | Replace hardcoded SQL `SA_PASSWORD` in `docker-compose.yml` with a `.env` variable | Medium | Open |
| 4 | Add `UserTypeId`-based authorization attributes/policies (currently checked ad hoc in controllers) | Medium | Open |
| 5 | Input validation on `AuthController.Register` — email format, password strength rules | Medium | Open |
| 6 | Fix `GetUserId()` in `OrdersController` — handle missing/malformed JWT claims with 401 instead of 500 | Medium | Open |

---

## 🐛 Bugs & Code Quality

| # | Task | Priority | Status |
|---|------|----------|--------|
| 1 | Upgrade `System.IdentityModel.Tokens.Jwt` from 7.0.3 (known vulnerability NU1902) | High | Open |
| 2 | Fix nullable-reference warnings (`CS8604`, `CS8602`) in `JwtService.cs`, `OrdersController.cs` | Low | Open |
| 3 | Add global exception-handling middleware — return `ProblemDetails` JSON instead of raw 500 | Medium | Open |
| 4 | Fix React Fast Refresh lint warning in `AuthContext.tsx` (exports both `AuthProvider` and `useAuth`) | Low | Open |
| 5 | Remove the orphaned `src/components/` folder at the solution root (stale duplicates of UI components, not part of any project) | Low | Open |
| 6 | Add an approve/reject action for the `ProductMaster` 3-step approval workflow (schema + "pending approvals" dashboard list exist; no UI to advance/deny) | Medium | Open |

---

## ⚡ Functional Gaps

| # | Task | Priority | Status |
|---|------|----------|--------|
| 1 | Conversation resume — persist `SessionId` across browser reloads (CLI resumes via `opencode`; API accepts a client `SessionId`, UI holds it in memory only) | Medium | Partial |
| 2 | Groq ↔ OpenRouter automatic fallback on 429/404 | Medium | Open |
| 3 | Refresh-token flow — silent JWT renewal before 24h expiry | Medium | Open |
| 4 | Pagination on `ProductsController.GetAll`, `OrdersController.GetOrders`, `CatalogController`, and `SalesOrdersController` | Medium | Open |
| 5 | `ConversationHistory` cleanup/retention job (grows unbounded) | Low | Open |
| 6 | Admin UI for login-audit data (login audit is now written to `EmployeeLogTable`/`CustomerLogTable`; no screen surfaces it yet) | Medium | Open |

---

## 🛒 Customer App Features

| # | Task | Priority | Status |
|---|------|----------|--------|
| 1 | Shopping cart as a first-class persisted entity (`Cart`/`CartItem`) | High | Done |
| 2 | Checkout with COD/UPI selection → SalesOrder + Payment + stock deduction (no real payment processing) | High | Done |
| 3 | Order tracking page + employee status advancement (Placed → Processing → Shipped → Delivered / Cancelled) | Medium | Done |
| 4 | Customer profile page (edit address, phone) | Low | Done |
| 5 | Product browse with search and category filters (catalog list exists, no search/filter) | Medium | Partial |
| 6 | Product reviews/ratings | Low | Open |
| 7 | Wishlist / reorder from past orders | Low | Open |
| 8 | Real payment integration (UPI/Card gateway) to resolve `PaymentStatus` | Medium | Open |

---

## 👨‍💼 Employee Dashboard Features

| # | Task | Priority | Status |
|---|------|----------|--------|
| 1 | Dashboard landing page (summary stats, low stock, pending approvals) | Medium | Done |
| 2 | CRUD pages for master tables: Product, Category, SubCategory, Unit, Warehouse, Vendor, RawMaterial, Department, UserType, Customer, Employee | High | Done |
| 3 | Product 3-step approval actions (advance/deny `Approval1At/2At/3At`) | High | Partial |
| 4 | Inventory management — stock transfers, adjustments, low-stock alerts (schema complete; low-stock list on dashboard only) | Medium | Partial |
| 5 | Purchase order management (schema + models exist; no controller/UI) | Medium | Open |
| 6 | Sales order management UI (API exists — `PATCH /api/sales-orders/{id}/status`) | Medium | Partial |
| 7 | Credit/Debit note workflows (schema exists; no API/UI) | Low | Open |
| 8 | Receipts (`Receipt`/`Payment` reconciliation) | Low | Open |

---

## 🧪 Testing

| # | Task | Priority | Status |
|---|------|----------|--------|
| 1 | Add real test coverage for `AgentHarness` and `DevTools` | High | Open |
| 2 | Auth flow tests — registration, login, JWT validation, employee/customer claim checks | High | Open |
| 3 | Order flow tests — cart → checkout → SalesOrder → stock deduction → status updates | Medium | Open |
| 4 | Agent permission tests — write/exec approval, customer 403, admin-only tools | High | Open |
| 5 | UI component tests (React / Vitest) | Low | Open |

> The test project (`tests/AI-Ecommerce.Tests`) currently contains only an
> empty xUnit stub — nothing fails, but nothing is verified either.

---

## 🏗️ Infrastructure & DevOps

| # | Task | Priority | Status |
|---|------|----------|--------|
| 1 | Add `ui` service to `docker-compose.yml` (full stack: api + ui + sql + adminer in one command) | Medium | Open |
| 2 | CORS config — make configurable instead of hardcoded `localhost:5173` | Low | Open |
| 3 | CI/CD pipeline — GitHub Actions for build, test, lint on PR | Medium | Open |
| 4 | Production Dockerfile hardening (non-root user, health checks, migrate-on-start) | Low | Open |

---

## 📊 Data & Schema

| # | Task | Priority | Status |
|---|------|----------|--------|
| 1 | Remove legacy `Products`/`Orders`/`OrderItems` tables once the new flow fully replaces them (kept for backward compat today) | Medium | Open |
| 2 | Remove `Working/diagnostics/build` and `Working/diagnostics/test` from git history (large binary bloat) | Low | Open |
| 3 | Seed `SubCategoryMaster` data in Excel (table exists but empty) | Low | Open |
| 4 | Add data-retention/indexing review for `StockTransaction`, `LedgerEntry`, `ConversationHistory` as they grow | Low | Open |

---

## Recommended next actions (pick any)

1. **Agent pending-approval workflow** — the largest security gap (API
   currently auto-approves).
2. **Product approval actions UI** — completes the merchandising loop and
   unclogs the dashboard's pending-approvals list.
3. **Test coverage** — at minimum auth + agent-permission tests; the test
   project is an empty stub.
4. **Upgrade JWT dependency** — known vulnerability (NU1902).
5. **Conversation resume across reloads** — small frontend change
   (persist `SessionId` to `sessionStorage`/`localStorage`).