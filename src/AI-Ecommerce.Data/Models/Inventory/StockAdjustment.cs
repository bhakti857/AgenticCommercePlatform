using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AI_Ecommerce.Data.Models.Inventory
{
    /// <summary>
    /// Manual stock correction (cycle count / damage / write-off). AdjustmentType:
    /// COUNT (set to counted value), IN (increase), OUT (decrease).
    /// </summary>
    public class StockAdjustment
    {
        [Key]
        public long StockAdjustmentId { get; set; }

        [Required]
        [MaxLength(50)]
        public string AdjustmentNo { get; set; } = string.Empty;

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int WarehouseId { get; set; }

        [Required]
        [MaxLength(20)]
        public string AdjustmentType { get; set; } = "COUNT";

        [Column(TypeName = "decimal(18,3)")]
        public decimal Quantity { get; set; }

        [MaxLength(500)]
        public string? Reason { get; set; }

        public long? AdjustedBy { get; set; }
        public DateTime AdjustedAt { get; set; } = DateTime.UtcNow;
    }
}