using AI_Ecommerce.Data.Models;
using AI_Ecommerce.Data.Models.Masters;
using AI_Ecommerce.Data.Models.Inventory;
using AI_Ecommerce.Data.Utils;
using Microsoft.EntityFrameworkCore;

namespace AI_Ecommerce.Data
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            // Seed a deterministic MasterAdmin (EmployeeMaster, UserTypeId 1) so the
            // same login exists identically on every machine that runs migrations +
            // seed. This lets the database baseline travel with the repo via git: any
            // machine becomes a self-hosted copy after `dotnet ef database update` +
            // startup seed.
            //
            // SECURITY NOTE: The credential below is intentionally FIXED and committed
            // so it is identical across environments. That means the password is
            // published in the repo and must be treated as a dev/demo credential.
            // Change the password after first login for any real (non-demo) use.
            const string masterAdminEmail = "bhaktiraut857@gmail.com";
            const string masterAdminPassword = "Saiyukta@1";

            var existingAdmin = await context.EmployeeMasters
                .FirstOrDefaultAsync(e => e.Email == masterAdminEmail);
            if (existingAdmin == null)
            {
                var masterAdmin = new EmployeeMaster
                {
                    Email = masterAdminEmail,
                    PasswordHash = PasswordHasher.HashPassword(masterAdminPassword),
                    FirstName = "Bhaktiraut",
                    LastName = "Raut",
                    DepartmentId = 1, // CEO
                    UserTypeId = 1, // MasterAdmin
                    IsActive = true
                };
                context.EmployeeMasters.Add(masterAdmin);
                await context.SaveChangesAsync();

                Console.WriteLine();
                Console.WriteLine("============================================================");
                Console.WriteLine(" Seeded fixed MasterAdmin account (portable baseline):");
                Console.WriteLine($"   Email:    {masterAdminEmail}");
                Console.WriteLine("   Password: <fixed per DataSeeder - change after first login>");
                Console.WriteLine("============================================================");
                Console.WriteLine();
            }

            // Seed a demo customer so the storefront/checkout can be tested immediately.
            if (!await context.CustomerMasters.AnyAsync(c => c.Email == "demo@example.com"))
            {
                context.CustomerMasters.Add(new CustomerMaster
                {
                    Email = "demo@example.com",
                    PasswordHash = PasswordHasher.HashPassword("Demo@1234"),
                    FirstName = "Demo",
                    LastName = "Customer",
                    PhoneNumber = "9876543210",
                    AddressLine = "1 Demo Street",
                    City = "Pune",
                    State = "Maharashtra",
                    Country = "India",
                    Pincode = "411001",
                    IsActive = true
                });
                await context.SaveChangesAsync();
            }

            // Seed the new-flow master/inventory data (ProductMaster + ProductStock).
            await SeedCatalogAsync(context);

            // Seed some legacy sample products (kept for the old ProductsController)
            if (!await context.Products.AnyAsync())
            {
                var products = new List<Product>
                {
                    new Product { SKU = "P001", Name = "Laptop", Price = 999.99m, Cost = 800m, Category = "Electronics", StockQuantity = 10, IsActive = true },
                    new Product { SKU = "P002", Name = "Smartphone", Price = 699.99m, Cost = 500m, Category = "Electronics", StockQuantity = 15, IsActive = true },
                    new Product { SKU = "P003", Name = "Headphones", Price = 89.99m, Cost = 60m, Category = "Audio", StockQuantity = 50, IsActive = true }
                };
                context.Products.AddRange(products);
                await context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Seeds the reference data the new storefront/cart/checkout depends on:
        /// a default warehouse, categories, units, sellable ProductMaster rows
        /// (fully approved so they show in the catalog), and ProductStock levels.
        /// </summary>
        private static async Task SeedCatalogAsync(ApplicationDbContext context)
        {
            // Default warehouse
            if (!await context.WarehouseMasters.AnyAsync())
            {
                context.WarehouseMasters.Add(new WarehouseMaster
                {
                    WarehouseName = "Main Warehouse",
                    Address = "Industrial Area",
                    City = "Pune",
                    State = "Maharashtra",
                    Pincode = "411001",
                    IsActive = true
                });
                await context.SaveChangesAsync();
            }
            var warehouse = await context.WarehouseMasters.FirstAsync();
            var warehouseId = (int)warehouse.WarehouseId;

            // Units + categories
            if (!await context.UnitMasters.AnyAsync())
            {
                context.UnitMasters.AddRange(
                    new UnitMaster { UnitName = "Piece" },
                    new UnitMaster { UnitName = "Kg" },
                    new UnitMaster { UnitName = "Litre" },
                    new UnitMaster { UnitName = "Box" });
                await context.SaveChangesAsync();
            }
            if (!await context.CategoryMasters.AnyAsync())
            {
                context.CategoryMasters.AddRange(
                    new CategoryMaster { CategoryName = "Electronics" },
                    new CategoryMaster { CategoryName = "Audio" },
                    new CategoryMaster { CategoryName = "Accessories" });
                await context.SaveChangesAsync();
            }

            var pieceUnitId = (await context.UnitMasters.FirstAsync(u => u.UnitName == "Piece")).UnitId;
            var electronicsId = (await context.CategoryMasters.FirstAsync(c => c.CategoryName == "Electronics")).CategoryId;
            var audioId = (await context.CategoryMasters.FirstAsync(c => c.CategoryName == "Audio")).CategoryId;

            // Sellable products (approval workflow marked complete)
            if (!await context.ProductMasters.AnyAsync())
            {
                var now = DateTime.UtcNow;
                context.ProductMasters.AddRange(
                    new ProductMaster { ProductCode = "PRD-001", ProductName = "Laptop", CategoryId = electronicsId, UnitId = pieceUnitId, PurchasePrice = 700.00m, SellingPrice = 999.99m, GSTPercent = 18.00m, IsActive = true, Approval1By = 1, Approval1At = now, Approval2By = 1, Approval2At = now, Approval3By = 1, Approval3At = now },
                    new ProductMaster { ProductCode = "PRD-002", ProductName = "Smartphone", CategoryId = electronicsId, UnitId = pieceUnitId, PurchasePrice = 450.00m, SellingPrice = 699.99m, GSTPercent = 18.00m, IsActive = true, Approval1By = 1, Approval1At = now, Approval2By = 1, Approval2At = now, Approval3By = 1, Approval3At = now },
                    new ProductMaster { ProductCode = "PRD-003", ProductName = "Headphones", CategoryId = audioId, UnitId = pieceUnitId, PurchasePrice = 50.00m, SellingPrice = 89.99m, GSTPercent = 12.00m, IsActive = true, Approval1By = 1, Approval1At = now, Approval2By = 1, Approval2At = now, Approval3By = 1, Approval3At = now }
                );
                await context.SaveChangesAsync();
            }

            // Stock levels for the seeded products in the default warehouse
            if (!await context.ProductStocks.AnyAsync())
            {
                var products = await context.ProductMasters.ToListAsync();
                foreach (var product in products)
                {
                    context.ProductStocks.Add(new ProductStock
                    {
                        ProductId = product.ProductId,
                        WarehouseId = warehouseId,
                        Quantity = 50,
                        ReservedQuantity = 0
                    });
                }
                await context.SaveChangesAsync();
            }
        }
    }
}