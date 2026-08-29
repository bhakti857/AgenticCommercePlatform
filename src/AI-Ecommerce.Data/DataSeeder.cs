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
            // Seed MasterAdmin (EmployeeMaster, UserTypeId 1) if not exists
            if (!await context.EmployeeMasters.AnyAsync(e => e.UserTypeId == 1))
            {
                // Never hardcode a known admin password — generate a random one per
                // environment and print it once so the operator can log in and
                // change it. Anyone who can read the console/logs at seed time is
                // assumed to be the trusted operator.
                var generatedPassword = GenerateRandomPassword();

                var masterAdmin = new EmployeeMaster
                {
                    Email = "masteradmin@example.com",
                    PasswordHash = PasswordHasher.HashPassword(generatedPassword),
                    FirstName = "Master",
                    LastName = "Admin",
                    DepartmentId = 1, // CEO
                    UserTypeId = 1, // MasterAdmin
                    IsActive = true
                };
                context.EmployeeMasters.Add(masterAdmin);
                await context.SaveChangesAsync();

                Console.WriteLine();
                Console.WriteLine("============================================================");
                Console.WriteLine(" Seeded MasterAdmin account (first run only):");
                Console.WriteLine($"   Email:    {masterAdmin.Email}");
                Console.WriteLine($"   Password: {generatedPassword}");
                Console.WriteLine(" Log in and change this password immediately — it will not");
                Console.WriteLine(" be shown again.");
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

        private static string GenerateRandomPassword()
        {
            // 24 random bytes -> 32-char base64-ish string, then ensure it satisfies
            // typical complexity rules by appending a fixed set of required classes.
            var bytes = System.Security.Cryptography.RandomNumberGenerator.GetBytes(18);
            var random = Convert.ToBase64String(bytes).Replace("+", "A").Replace("/", "b").Replace("=", "9");
            return $"{random}!1";
        }
    }
}