# Database Portability — How the DB + Data Travel Between Machines

## Goal
Whoever is working on the project should be able to become the **host**:
connect to their own local SQL, get the *same schema and baseline data*, and not
depend on another machine being online.

## How it works
- **Schema travels via git** — as committed EF Core migrations
  (`src/AI-Ecommerce.Data/Migrations`). Every machine that applies migrations
  gets the identical table structure.
- **Baseline data travels via git too** — `DataSeeder` (committed) creates the
  same rows on every machine on startup:
  - MasterAdmin: `bhaktiraut857@gmail.com` / `Saiyukta@1` (UserTypeId 1)
  - Demo customer: `demo@example.com` / `Demo@1234`
  - Departments, user types, warehouse, units, categories, sellable products,
    and stock levels.
- Each machine runs migration + seed against its **own local SQL container**,
  becoming an independent host with an identical starting point.

## Switching hosts (e.g. moving work from Machine A to Machine B)

### On the new machine (say Machine B), first time:
```bash
# 1. Start your own local SQL Server
docker-compose up -d sql-server

# 2. Apply the schema (creates all tables)
cd src/AI-Ecommerce.Data
dotnet ef database update --startup-project ..\AI-Ecommerce.Cli

# 3. Run the app — DataSeeder seeds the baseline automatically on startup
cd ..\AI-Ecommerce.Api
dotnet run
```

The new machine is now the host with the same schema + baseline data as the old
one. Log in with:
- `bhaktiraut857@gmail.com` / `Saiyukta@1` (MasterAdmin)
- `demo@example.com` / `Demo@1234` (customer)

## IMPORTANT — what travels and what does NOT
| Thing | Travels via git? | Notes |
|---|---|---|
| Schema (tables, columns) | ✅ Yes | EF migrations are committed |
| Seed/baseline data (admin, customer, product catalog) | ✅ Yes | `DataSeeder` is committed |
| New/changed rows you create AFTER seeding (orders, new records) | ❌ No | Live in that machine's local Docker volume only |

This is the key trade-off of the portable-baseline approach: every machine starts
identical, but **data you add later stays on the machine where you added it**. If
you switch machines, the new machine will have the baseline but NOT the records
you created since seeding.

## If you need to carry your current data (not just the baseline)
Export the whole database from the current host and restore it on the other:
```bash
# On current host — back up
docker exec ecommerce-sql /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa \
  -P 'YourStrong!Passw0rd' -C -d AgenticCommerceDB \
  -Q "BACKUP DATABASE [AgenticCommerceDB] TO DISK='/var/opt/mssql/data/AgenticCommerceDB.bak' WITH INIT"
docker cp ecommerce-sql:/var/opt/mssql/data/AgenticCommerceDB.bak .\AgenticCommerceDB.bak

# Copy the .bak to the new machine, then restore there:
docker cp .\AgenticCommerceDB.bak ecommerce-sql:/var/opt/mssql/data/AgenticCommerceDB.bak
docker exec ecommerce-sql /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa \
  -P 'YourStrong!Passw0rd' -C \
  -Q "RESTORE DATABASE [AgenticCommerceDB] FROM DISK='/var/opt/mssql/data/AgenticCommerceDB.bak' WITH REPLACE"
```
This gives an exact full copy, not just the baseline.

## Security note
The MasterAdmin credential above is **fixed and committed** so it is identical on
every machine. That means the password is visible in the repo. It is intended as a
dev/demo credential — change the password after first login for any real use.
