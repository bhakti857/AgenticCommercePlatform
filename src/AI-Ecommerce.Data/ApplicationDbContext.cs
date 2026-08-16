using AI_Ecommerce.Data.Models;
using AI_Ecommerce.Data.Models.Masters;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace AI_Ecommerce.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ConversationHistory> ConversationHistories { get; set; }

        // --- New Master/reference tables (see AGENTS.md / FutureScope.md for the
        // phased migration plan away from the legacy Users/Product tables above). ---
        public DbSet<CustomerMaster> CustomerMasters { get; set; }
        public DbSet<EmployeeMaster> EmployeeMasters { get; set; }
        public DbSet<DepartmentMaster> DepartmentMasters { get; set; }
        public DbSet<UserTypeMaster> UserTypeMasters { get; set; }
        public DbSet<EmployeeLogTable> EmployeeLogs { get; set; }
        public DbSet<CustomerLogTable> CustomerLogs { get; set; }
        public DbSet<CategoryMaster> CategoryMasters { get; set; }
        public DbSet<SubCategoryMaster> SubCategoryMasters { get; set; }
        public DbSet<UnitMaster> UnitMasters { get; set; }
        public DbSet<WarehouseMaster> WarehouseMasters { get; set; }
        public DbSet<VendorMaster> VendorMasters { get; set; }
        public DbSet<ProductMaster> ProductMasters { get; set; }
        public DbSet<RawMaterialMaster> RawMaterialMasters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Product configuration
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.SKU).IsUnique();
                entity.HasIndex(e => e.Category);
            });

            // Order configuration
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasIndex(e => e.OrderNumber).IsUnique();
                entity.HasIndex(e => e.CustomerId);
                entity.HasIndex(e => e.OrderStatus);
                entity.HasOne(e => e.Customer)
                    .WithMany()
                    .HasForeignKey(e => e.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // OrderItem configuration
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasOne(e => e.Order)
                    .WithMany(e => e.OrderItems)
                    .HasForeignKey(e => e.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Product)
                    .WithMany()
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ConversationHistory>(entity =>
            {
                entity.HasIndex(e => e.SessionId);
                entity.HasIndex(e => e.UserId);
            });

            ConfigureMasterEntities(modelBuilder);
        }

        private static void ConfigureMasterEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerMaster>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.UniqueId).IsUnique();
            });

            modelBuilder.Entity<EmployeeMaster>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.UniqueId).IsUnique();

                entity.HasOne(e => e.Department)
                    .WithMany()
                    .HasForeignKey(e => e.DepartmentId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.UserType)
                    .WithMany()
                    .HasForeignKey(e => e.UserTypeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<DepartmentMaster>(entity =>
            {
                entity.HasIndex(e => e.DepartmentName).IsUnique();
                var seededAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                entity.HasData(
                    new DepartmentMaster { DepartmentId = 1, DepartmentName = "CEO", CreatedAt = seededAt },
                    new DepartmentMaster { DepartmentId = 2, DepartmentName = "Software Developer", CreatedAt = seededAt }
                );
            });

            modelBuilder.Entity<UserTypeMaster>(entity =>
            {
                entity.HasIndex(e => e.UserTypeName).IsUnique();
                var seededAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                entity.HasData(
                    new UserTypeMaster { UserTypeId = 1, UserTypeName = "MasterAdmin", CreatedAt = seededAt },
                    new UserTypeMaster { UserTypeId = 2, UserTypeName = "Admin", CreatedAt = seededAt },
                    new UserTypeMaster { UserTypeId = 3, UserTypeName = "Senior", CreatedAt = seededAt },
                    new UserTypeMaster { UserTypeId = 4, UserTypeName = "Junior", CreatedAt = seededAt },
                    new UserTypeMaster { UserTypeId = 5, UserTypeName = "User", CreatedAt = seededAt }
                );
            });

            modelBuilder.Entity<EmployeeLogTable>(entity =>
            {
                entity.HasIndex(e => e.EmployeeId);
                entity.HasOne(e => e.Employee)
                    .WithMany()
                    .HasForeignKey(e => e.EmployeeId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<CustomerLogTable>(entity =>
            {
                entity.HasIndex(e => e.CustomerId);
                entity.HasOne(e => e.Customer)
                    .WithMany()
                    .HasForeignKey(e => e.CustomerId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<CategoryMaster>(entity =>
            {
                entity.HasIndex(e => e.CategoryName).IsUnique();
            });

            modelBuilder.Entity<SubCategoryMaster>(entity =>
            {
                entity.HasIndex(e => new { e.CategoryId, e.SubCategoryName }).IsUnique();
                entity.HasOne(e => e.Category)
                    .WithMany()
                    .HasForeignKey(e => e.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<UnitMaster>(entity =>
            {
                entity.HasIndex(e => e.UnitName).IsUnique();
            });

            modelBuilder.Entity<WarehouseMaster>(entity =>
            {
                entity.HasIndex(e => e.WarehouseName).IsUnique();
            });

            modelBuilder.Entity<VendorMaster>(entity =>
            {
                entity.HasIndex(e => e.VendorName);
            });

            modelBuilder.Entity<ProductMaster>(entity =>
            {
                entity.HasIndex(e => e.ProductCode).IsUnique();

                entity.HasOne(e => e.Category)
                    .WithMany()
                    .HasForeignKey(e => e.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.SubCategory)
                    .WithMany()
                    .HasForeignKey(e => e.SubCategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Unit)
                    .WithMany()
                    .HasForeignKey(e => e.UnitId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<RawMaterialMaster>(entity =>
            {
                entity.HasIndex(e => e.RawMaterialCode).IsUnique();

                entity.HasOne(e => e.Unit)
                    .WithMany()
                    .HasForeignKey(e => e.UnitId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}