using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AI_Ecommerce.Data.Models.Transactions
{
    /// <summary>
    /// Payment attempt for a sales order. Per requirements, no real payment
    /// processing is added — the record is created at checkout (COD or UPI)
    /// and left in "Pending" status.
    /// </summary>
    public class Payment
    {
        [Key]
        public long PaymentId { get; set; }

        [Required]
        public long SalesOrderId { get; set; }

        [Required]
        public long CustomerId { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        /// <summary>"COD" or "UPI"</summary>
        [Required]
        [MaxLength(20)]
        public string PaymentMethod { get; set; } = "COD";

        /// <summary>Pending → Received / Failed. Kept Pending unless manually confirmed.</summary>
        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "Pending";

        [MaxLength(100)]
        public string? ReferenceNumber { get; set; }

        [MaxLength(500)]
        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public SalesOrder? SalesOrder { get; set; }
    }
}