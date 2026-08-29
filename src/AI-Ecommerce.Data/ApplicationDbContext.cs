using AI_Ecommerce.Data.Models;
using AI_Ecommerce.Data.Models.Accounting;
using AI_Ecommerce.Data.Models.Cart;
using AI_Ecommerce.Data.Models.Inventory;
using AI_Ecommerce.Data.Models.Masters;
using AI_Ecommerce.Data.Models.Transactions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AI_Ecommerce.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // --- Legacy tables (kept for backward compatibility during migration) ---
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ConversationHistory> ConversationHistories { get; set; }

        // --- Master/reference tables ---
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

        // --- Transaction tables ---
        public DbSet<SalesOrder> SalesOrders { get; set; }
        public DbSet<SalesOrderItem> SalesOrderItems { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<CreditNote> CreditNotes { get; set; }
        public DbSet<CreditNoteItem> CreditNoteItems { get; set; }
        public DbSet<DebitNote> DebitNotes { get; set; }
        public DbSet<DebitNoteItem> DebitNoteItems { get; set; }

        // --- Inventory tables ---
        public DbSet<ProductStock> ProductStocks { get; set; }
        public DbSet<RawMaterialStock> RawMaterialStocks { get; set; }
        public DbSet<StockTransaction> StockTransactions { get; set; }
        public DbSet<StockTransfer> StockTransfers { get; set; }
        public DbSet<StockAdjustment> StockAdjustments { get; set; }

        // --- Accounting tables ---
        public DbSet<Ledger> Ledgers { get; set; }
        public DbSet<LedgerEntry> LedgerEntries { get; set; }

        // --- Cart ---
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

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
            ConfigureTransactionEntities(modelBuilder);
            ConfigureInventoryEntities(modelBuilder);
            ConfigureAccountingEntities(modelBuilder);
            ConfigureCartEntities(modelBuilder);
        }

        private static void ConfigureMasterEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerMaster>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.UniqueId).IsUnique();
                entity.HasQueryFilter(e => e.DeletedAt == null);
            });

            modelBuilder.Entity<EmployeeMaster>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.UniqueId).IsUnique();
                entity.HasQueryFilter(e => e.DeletedAt == null);

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
                entity.HasQueryFilter(e => e.DeletedAt == null);
                var seededAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                entity.HasData(
                    new DepartmentMaster { DepartmentId = 1, DepartmentName = "CEO", CreatedAt = seededAt },
                    new DepartmentMaster { DepartmentId = 2, DepartmentName = "Software Developer", CreatedAt = seededAt }
                );
            });

            modelBuilder.Entity<UserTypeMaster>(entity =>
            {
                entity.HasIndex(e => e.UserTypeName).IsUnique();
                entity.HasQueryFilter(e => e.DeletedAt == null);
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
                entity.HasQueryFilter(e => e.DeletedAt == null);
            });

            modelBuilder.Entity<SubCategoryMaster>(entity =>
            {
                entity.HasIndex(e => new { e.CategoryId, e.SubCategoryName }).IsUnique();
                entity.HasQueryFilter(e => e.DeletedAt == null);
                entity.HasOne(e => e.Category)
                    .WithMany()
                    .HasForeignKey(e => e.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<UnitMaster>(entity =>
            {
                entity.HasIndex(e => e.UnitName).IsUnique();
                entity.HasQueryFilter(e => e.DeletedAt == null);
            });

            modelBuilder.Entity<WarehouseMaster>(entity =>
            {
                entity.HasIndex(e => e.WarehouseName).IsUnique();
                entity.HasQueryFilter(e => e.DeletedAt == null);
            });

            modelBuilder.Entity<VendorMaster>(entity =>
            {
                entity.HasIndex(e => e.VendorName);
                entity.HasQueryFilter(e => e.DeletedAt == null);
            });

            modelBuilder.Entity<ProductMaster>(entity =>
            {
                entity.HasIndex(e => e.ProductCode).IsUnique();
                entity.HasQueryFilter(e => e.DeletedAt == null);

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
                entity.HasQueryFilter(e => e.DeletedAt == null);

                entity.HasOne(e => e.Unit)
                    .WithMany()
                    .HasForeignKey(e => e.UnitId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private static void ConfigureTransactionEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SalesOrder>(entity =>
            {
                entity.HasIndex(e => e.SalesOrderNo).IsUnique();
                entity.HasIndex(e => e.CustomerId);
                entity.HasIndex(e => e.OrderStatus);
                entity.HasIndex(e => e.PaymentStatus);

                entity.HasOne(e => e.Customer)
                    .WithMany()
                    .HasForeignKey(e => e.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Items)
                    .WithOne(e => e.SalesOrder)
                    .HasForeignKey(e => e.SalesOrderId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<SalesOrderItem>(entity =>
            {
                entity.HasIndex(e => e.SalesOrderId);
                entity.HasIndex(e => e.ProductId);

                entity.HasOne(e => e.Product)
                    .WithMany()
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<PurchaseOrder>(entity =>
            {
                entity.HasIndex(e => e.PurchaseOrderNo).IsUnique();
                entity.HasIndex(e => e.VendorId);

                entity.HasOne(e => e.Vendor)
                    .WithMany()
                    .HasForeignKey(e => e.VendorId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Items)
                    .WithOne(e => e.PurchaseOrder)
                    .HasForeignKey(e => e.PurchaseOrderId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PurchaseOrderItem>(entity =>
            {
                entity.HasIndex(e => e.PurchaseOrderId);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasIndex(e => e.SalesOrderId);
                entity.HasIndex(e => e.CustomerId);

                entity.HasOne(e => e.SalesOrder)
                    .WithMany()
                    .HasForeignKey(e => e.SalesOrderId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Receipt>(entity =>
            {
                entity.HasIndex(e => e.ReceiptNo).IsUnique();
                entity.HasIndex(e => e.PaymentId);

                entity.HasOne(e => e.Payment)
                    .WithMany()
                    .HasForeignKey(e => e.PaymentId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<CreditNote>(entity =>
            {
                entity.HasIndex(e => e.CreditNoteNo).IsUnique();
                entity.HasIndex(e => e.SalesOrderId);
                entity.HasIndex(e => e.CustomerId);

                entity.HasOne(e => e.SalesOrder)
                    .WithMany()
                    .HasForeignKey(e => e.SalesOrderId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Items)
                    .WithOne(e => e.CreditNote)
                    .HasForeignKey(e => e.CreditNoteId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<CreditNoteItem>(entity =>
            {
                entity.HasIndex(e => e.CreditNoteId);
            });

            modelBuilder.Entity<DebitNote>(entity =>
            {
                entity.HasIndex(e => e.DebitNoteNo).IsUnique();
                entity.HasIndex(e => e.PurchaseOrderId);
                entity.HasIndex(e => e.VendorId);

                entity.HasOne(e => e.PurchaseOrder)
                    .WithMany()
                    .HasForeignKey(e => e.PurchaseOrderId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Items)
                    .WithOne(e => e.DebitNote)
                    .HasForeignKey(e => e.DebitNoteId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<DebitNoteItem>(entity =>
            {
                entity.HasIndex(e => e.DebitNoteId);
            });
        }

        private static void ConfigureInventoryEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductStock>(entity =>
            {
                entity.HasIndex(e => new { e.ProductId, e.WarehouseId }).IsUnique();

                entity.HasOne(e => e.Product)
                    .WithMany()
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Warehouse)
                    .WithMany()
                    .HasForeignKey(e => e.WarehouseId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<RawMaterialStock>(entity =>
            {
                entity.HasIndex(e => new { e.RawMaterialId, e.WarehouseId }).IsUnique();

                entity.HasOne(e => e.RawMaterial)
                    .WithMany()
                    .HasForeignKey(e => e.RawMaterialId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Warehouse)
                    .WithMany()
                    .HasForeignKey(e => e.WarehouseId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<StockTransaction>(entity =>
            {
                entity.HasIndex(e => new { e.ProductId, e.TransactionDate });
                entity.HasIndex(e => e.WarehouseId);
                entity.HasIndex(e => e.TransactionType);
            });

            modelBuilder.Entity<StockTransfer>(entity =>
            {
                entity.HasIndex(e => e.TransferNo).IsUnique();
                entity.HasIndex(e => e.FromWarehouseId);
                entity.HasIndex(e => e.ToWarehouseId);
            });

            modelBuilder.Entity<StockAdjustment>(entity =>
            {
                entity.HasIndex(e => e.AdjustmentNo).IsUnique();
                entity.HasIndex(e => e.ProductId);
                entity.HasIndex(e => e.WarehouseId);
            });
        }

        private static void ConfigureAccountingEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ledger>(entity =>
            {
                entity.HasIndex(e => e.LedgerName).IsUnique();
                entity.HasIndex(e => e.LedgerType);
            });

            modelBuilder.Entity<LedgerEntry>(entity =>
            {
                entity.HasIndex(e => new { e.LedgerId, e.EntryDate });
                entity.HasIndex(e => new { e.ReferenceType, e.ReferenceId });

                entity.HasOne(e => e.Ledger)
                    .WithMany()
                    .HasForeignKey(e => e.LedgerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private static void ConfigureCartEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasIndex(e => e.CustomerId).IsUnique();

                entity.HasOne(e => e.Customer)
                    .WithMany()
                    .HasForeignKey(e => e.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.Items)
                    .WithOne(e => e.Cart)
                    .HasForeignKey(e => e.CartId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.HasIndex(e => new { e.CartId, e.ProductId }).IsUnique();
                entity.HasIndex(e => e.ProductId);

                entity.HasOne(e => e.Product)
                    .WithMany()
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}