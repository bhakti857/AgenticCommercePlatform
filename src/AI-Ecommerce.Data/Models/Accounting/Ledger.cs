using System;
using System.ComponentModel.DataAnnotations;

namespace AI_Ecommerce.Data.Models.Accounting
{
    /// <summary>
    /// Ledger account (chart of accounts). LedgerType: Asset, Liability,
    /// Income, Expense. Payments/receipts/notes post double-entry rows here.
    /// </summary>
    public class Ledger
    {
        [Key]
        public long LedgerId { get; set; }

        [Required]
        [MaxLength(150)]
        public string LedgerName { get; set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string LedgerType { get; set; } = "Asset";

        public bool IsActive { get; set; } = true;

        public long? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}