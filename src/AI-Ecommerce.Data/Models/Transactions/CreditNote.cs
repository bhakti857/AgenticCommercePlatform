using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AI_Ecommerce.Data.Models.Transactions
{
    /// <summary>Credit note issued against a sales order (e.g. returns/refunds to the customer).</summary>
    public class CreditNote
    {
        [Key]
        public long CreditNoteId { get; set; }

        [Required]
        [MaxLength(50)]
        public string CreditNoteNo { get; set; } = string.Empty;

        [Required]
        public long SalesOrderId { get; set; }

        [Required]
        public long CustomerId { get; set; }

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

        public SalesOrder? SalesOrder { get; set; }
        public List<CreditNoteItem>? Items { get; set; }
    }
}