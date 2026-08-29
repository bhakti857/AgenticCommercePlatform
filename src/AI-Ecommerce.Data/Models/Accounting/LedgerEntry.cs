using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AI_Ecommerce.Data.Models.Accounting
{
    /// <summary>
    /// Single debit/credit posting against a ledger. ReferenceType /
    /// ReferenceId link the entry back to the source document
    /// (SalesOrder, Payment, Receipt, CreditNote, DebitNote, PurchaseOrder).
    /// </summary>
    public class LedgerEntry
    {
        [Key]
        public long LedgerEntryId { get; set; }

        [Required]
        public long LedgerId { get; set; }

        public DateTime EntryDate { get; set; } = DateTime.UtcNow;

        [Required]
        [MaxLength(50)]
        public string ReferenceType { get; set; } = string.Empty;

        public long? ReferenceId { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DebitAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal CreditAmount { get; set; }

        public long? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Ledger? Ledger { get; set; }
    }
}