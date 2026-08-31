using AI_Ecommerce.Data.Models;
using AI_Ecommerce.Data.Models.Masters;
using AI_Ecommerce.Data.Models.Inventory;
using Microsoft.EntityFrameworkCore;

namespace AI_Ecommerce.Data
{
    /// <summary>
    /// Verifies that the database has been seeded via the Excel import script.
    /// All data now lives in schema/data.xlsx — this class only checks presence,
    /// it does NOT insert hardcoded data.
    /// </summary>
    public static class DataSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            var missing = new List<string>();

            if (!await context.DepartmentMasters.AnyAsync())
                missing.Add("DepartmentMasters");

            if (!await context.UserTypeMasters.AnyAsync())
                missing.Add("UserTypeMasters");

            if (!await context.EmployeeMasters.AnyAsync())
                missing.Add("EmployeeMasters (run import-from-excel.ps1)");

            if (!await context.CustomerMasters.AnyAsync())
                missing.Add("CustomerMasters (run import-from-excel.ps1)");

            if (!await context.WarehouseMasters.AnyAsync())
                missing.Add("WarehouseMasters (run import-from-excel.ps1)");

            if (!await context.CategoryMasters.AnyAsync())
                missing.Add("CategoryMasters");

            if (!await context.UnitMasters.AnyAsync())
                missing.Add("UnitMasters");

            if (!await context.ProductMasters.AnyAsync())
                missing.Add("ProductMasters");

            if (missing.Count > 0)
            {
                Console.WriteLine();
                Console.WriteLine("============================================================");
                Console.WriteLine(" WARNING: Missing seed data in: " + string.Join(", ", missing));
                Console.WriteLine(" Run: .\\scripts\\import-from-excel.ps1");
                Console.WriteLine(" to import all data from schema/data.xlsx");
                Console.WriteLine("============================================================");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Database seeded successfully from Excel.");
            }
        }
    }
}
