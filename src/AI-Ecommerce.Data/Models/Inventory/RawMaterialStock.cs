using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AI_Ecommerce.Data.Models.Masters;

namespace AI_Ecommerce.Data.Models.Inventory
{
    /// <summary>Per-warehouse stock level for a raw material.</summary>
    public class RawMaterialStock
    {
        [Key]
        public long RawMaterialStockId { get; set; }

        [Required]
        public int RawMaterialId { get; set; }

        [Required]
        public int WarehouseId { get; set; }

        [Column(TypeName = "decimal(18,3)")]
        public decimal Quantity { get; set; }

        [Column(TypeName = "decimal(18,3)")]
        public decimal ReservedQuantity { get; set; }

        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

        public RawMaterialMaster? RawMaterial { get; set; }
        public WarehouseMaster? Warehouse { get; set; }
    }
}