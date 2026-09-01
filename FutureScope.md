# Future Scope — Requirements & Backlog

Current state (Sep 2026): Master/Transaction/Inventory schema is in place,
auth is cut over to CustomerMaster/EmployeeMaster, Excel is the single
source of truth for seed data, and the agent is restricted to employees.

---

## 🔒 Security

| # | Task | Priority | Status |
|---|------|----------|--------|
| 1 | Build a real pending-approval workflow for agent WriteFile/ExecuteCommand (currently auto-approved in API) | High | Open |
| 2 | Rotate JWT secret that was previously committed to git history | High | Open |
| 3 | Replace hardcoded SQL `SA_PASSWORD` in `docker-compose.yml` with `.env` variable | Medium | Open |
| 4 | Add `UserType`-based authorization attributes/policies (currently checked ad hoc in `AgentController`) | Medium | Open |
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

---

## ⚡ Functional Gaps

| # | Task | Priority | Status |
|---|------|----------|--------|
| 1 | Conversation resume — persist `SessionId` so users can continue previous chats | Medium | Open |
| 2 | Groq ↔ OpenRouter automatic fallback on 429/404 | Medium | Open |
| 3 | Refresh-token flow — silent JWT renewal before 24h expiry | Medium | Open |
| 4 | Pagination on `ProductsController.GetAll` and `OrdersController.GetOrders` | Medium | Open |
| 5 | ConversationHistory cleanup/retention job (grows unbounded) | Low | Open |
| 6 | Login audit logging — wire `EmployeeLogTable`/`CustomerLogTable` (schema exists, nothing writes) | Medium | Open |

---

## 🛒 Customer App Features

| # | Task | Priority | Status |
|---|------|----------|--------|
| 1 | Shopping cart as first-class entity (persisted, not just client-supplied item list) | High | Open |
| 2 | Product browse with search, filter by category | Medium | Open |
| 3 | Checkout with COD/UPI selection (no real payment processing) | Medium | Open |
| 4 | Order tracking page | Medium | Open |
| 5 | Customer profile page (edit address, phone) | Low | Open |
| 6 | Product reviews/ratings | Low | Open |

---

## 👨‍💼 Employee Dashboard Features

| # | Task | Priority | Status |
|---|------|----------|--------|
| 1 | Dashboard landing page (summary stats) | Medium | Open |
| 2 | CRUD pages for all master tables: Product, Category, SubCategory, Unit, Warehouse, Vendor, RawMaterial, Department, UserType, Customer, Employee | High | Open |
| 3 | Inventory management — low-stock alerts, stock transfers, adjustments | Medium | Open |
| 4 | Sales/Purchase order management | Medium | Open |
| 5 | Credit/Debit note workflows | Low | Open |

---

## 🧪 Testing

| # | Task | Priority | Status |
|---|------|----------|--------|
| 1 | Add real test coverage for `AgentHarness` and `DevTools` | High | Open |
| 2 | Auth flow tests — registration, login, JWT validation, role gating | High | Open |
| 3 | Order flow tests — create, list, status updates | Medium | Open |
| 4 | Agent permission tests — write/exec approval, Customer 403 | High | Open |
| 5 | UI component tests (React) | Low | Open |

---

## 🏗️ Infrastructure & DevOps

| # | Task | Priority | Status |
|---|------|----------|--------|
| 1 | Add `ui` service to `docker-compose.yml` (full stack: api + ui + sql + adminer in one command) | Medium | Open |
| 2 | CORS config — make configurable instead of hardcoded `localhost:5173` | Low | Open |
| 3 | CI/CD pipeline — GitHub Actions for build, test, lint on PR | Medium | Open |
| 4 | Production Dockerfile hardening (non-root user, health checks) | Low | Open |

---

## 📊 Data & Schema

| # | Task | Priority | Status |
|---|------|----------|--------|
| 1 | Remove legacy `Products`/`Orders` tables (replaced by `ProductMaster`/`SalesOrders`) | Medium | Open |
| 2 | Remove `Working/diagnostics/build` and `Working/diagnostics/test` from git history (large binary bloat) | Low | Open |
| 3 | Seed `SubCategoryMaster` data in Excel (table exists but empty) | Low | Open |

---

## Recommended next actions (pick any)

1. **Customer cart + checkout** — highest user-facing value
2. **Employee CRUD dashboard** — needed to manage the product catalog
3. **Agent pending-approval workflow** — security gap
4. **Test coverage** — at minimum auth + agent permission tests
5. **Upgrade JWT dependency** — known vulnerability
