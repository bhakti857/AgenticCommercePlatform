using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AI_Ecommerce.Data.Models.Transactions
{
    /// <summary>Line item on a debit note, referencing the original purchase order line.</summary>
    public class DebitNoteItem
    {
        [Key]
        public long DebitNoteItemId { get; set; }

        [Required]
        public long DebitNoteId { get; set; }

        public long? PurchaseOrderItemId { get; set; }
        public int? ProductId { get; set; }
        public int? RawMaterialId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        public DebitNote? DebitNote { get; set; }
    }
}