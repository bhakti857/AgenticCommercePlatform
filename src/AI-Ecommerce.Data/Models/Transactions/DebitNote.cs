using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AI_Ecommerce.Data.Models.Transactions
{
    /// <summary>Debit note raised against a purchase order (e.g. damaged goods / supplier debit).</summary>
    public class DebitNote
    {
        [Key]
        public long DebitNoteId { get; set; }

        [Required]
        [MaxLength(50)]
        public string DebitNoteNo { get; set; } = string.Empty;

        [Required]
        public long PurchaseOrderId { get; set; }

        [Required]
        public int VendorId { get; set; }

        public DateTime NoteDate { get; set; } = DateTime.UtcNow;

        [MaxLength(500)]
        public string? Reason { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        /// <summary>Pending → Approved → Applied (or Rejected).</summary>
        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "Pending";

        public long? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public PurchaseOrder? PurchaseOrder { get; set; }
        public List<DebitNoteItem>? Items { get; set; }
    }
}