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
                var masterAdmin = new User
                {
                    Id = Guid.NewGuid(),
                    Email = "masteradmin@example.com",
                    PasswordHash = PasswordHasher.HashPassword("Admin@123"),
                    FirstName = "Master",
                    LastName = "Admin",
                    UserType = 1,
                    IsActive = true
                };
                context.Users.Add(masterAdmin);
                await context.SaveChangesAsync();
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
    }
}