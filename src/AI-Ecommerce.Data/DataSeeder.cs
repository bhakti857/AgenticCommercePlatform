using AI_Ecommerce.Data.Models;
using AI_Ecommerce.Data.Utils;
using Microsoft.EntityFrameworkCore;

namespace AI_Ecommerce.Data
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            // Seed MasterAdmin if not exists
            if (!await context.Users.AnyAsync(u => u.UserType == 1))
            {
                // Never hardcode a known admin password — generate a random one per
                // environment and print it once so the operator can log in and
                // change it. Anyone who can read the console/logs at seed time is
                // assumed to be the trusted operator.
                var generatedPassword = GenerateRandomPassword();

                var masterAdmin = new User
                {
                    Id = Guid.NewGuid(),
                    Email = "masteradmin@example.com",
                    PasswordHash = PasswordHasher.HashPassword(generatedPassword),
                    FirstName = "Master",
                    LastName = "Admin",
                    UserType = 1,
                    IsActive = true
                };
                context.Users.Add(masterAdmin);
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

            // Seed some sample products
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