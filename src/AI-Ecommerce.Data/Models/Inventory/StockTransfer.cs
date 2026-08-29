using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AI_Ecommerce.Data.Models.Inventory
{
    /// <summary>Request to move stock of one product between two warehouses.</summary>
    public class StockTransfer
    {
        [Key]
        public long StockTransferId { get; set; }

        [Required]
        [MaxLength(50)]
        public string TransferNo { get; set; } = string.Empty;

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int FromWarehouseId { get; set; }

        [Required]
        public int ToWarehouseId { get; set; }

        [Column(TypeName = "decimal(18,3)")]
        public decimal Quantity { get; set; }

        /// <summary>Pending → Completed (or Cancelled).</summary>
        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "Pending";

        [MaxLength(500)]
        public string? Notes { get; set; }

        public long? TransferredBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; }
    }
}