using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AI_Ecommerce.Data.Models.Masters;

namespace AI_Ecommerce.Data.Models.Inventory
{
    /// <summary>
    /// Per-warehouse stock level for a finished product. The live quantity is
    /// updated transactionally; every change is also recorded as a
    /// StockTransaction row for the audit trail.
    /// </summary>
    public class ProductStock
    {
        [Key]
        public long ProductStockId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int WarehouseId { get; set; }

        [Column(TypeName = "decimal(18,3)")]
        public decimal Quantity { get; set; }

        [Column(TypeName = "decimal(18,3)")]
        public decimal ReservedQuantity { get; set; }

        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

        public ProductMaster? Product { get; set; }
        public WarehouseMaster? Warehouse { get; set; }
    }
}