using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AI_Ecommerce.Data.Models.Inventory
{
    /// <summary>
    /// Immutable ledger of every stock movement for a product. TransactionType
    /// values: IN (received), OUT (sold/shipped), TRANSFER_IN, TRANSFER_OUT,
    /// ADJUSTMENT_IN, ADJUSTMENT_OUT. ReferenceId points at the source document
    /// (e.g. SalesOrderId, PurchaseOrderId, StockTransferId).
    /// </summary>
    public class StockTransaction
    {
        [Key]
        public long StockTransactionId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int WarehouseId { get; set; }

        [Required]
        [MaxLength(30)]
        public string TransactionType { get; set; } = "IN";

        public long? ReferenceId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

        [MaxLength(500)]
        public string? Notes { get; set; }

        public long? CreatedBy { get; set; }
    }
}