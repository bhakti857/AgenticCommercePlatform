using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AI_Ecommerce.Data.Models.Transactions
{
    /// <summary>Line item on a credit note, referencing the original sales order line.</summary>
    public class CreditNoteItem
    {
        [Key]
        public long CreditNoteItemId { get; set; }

        [Required]
        public long CreditNoteId { get; set; }

        public long? SalesOrderItemId { get; set; }
        public int? ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        public CreditNote? CreditNote { get; set; }
    }
}