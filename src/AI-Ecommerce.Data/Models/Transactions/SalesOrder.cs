using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AI_Ecommerce.Data.Models.Masters;

namespace AI_Ecommerce.Data.Models.Transactions
{
    /// <summary>
    /// Sales order header — the customer-facing order created at checkout.
    /// Replaces the legacy Order table in the new flow:
    /// Customer → SalesOrder → SalesOrderItem → Product → ProductStock → StockTransaction.
    /// </summary>
    public class SalesOrder
    {
        [Key]
        public long SalesOrderId { get; set; }

        [Required]
        [MaxLength(50)]
        public string SalesOrderNo { get; set; } = string.Empty;

        [Required]
        public long CustomerId { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "decimal(18,2)")]
        public decimal SubTotal { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TaxAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ShippingCost { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        /// <summary>Payment method selected at checkout: "COD" or "UPI". No real payment is processed — stays pending.</summary>
        [Required]
        [MaxLength(20)]
        public string PaymentMethod { get; set; } = "COD";

        [Required]
        [MaxLength(20)]
        public string PaymentStatus { get; set; } = "Pending";

        /// <summary>Order lifecycle: Placed → Processing → Shipped → Delivered (or Cancelled).</summary>
        [Required]
        [MaxLength(30)]
        public string OrderStatus { get; set; } = "Placed";

        // Billing address snapshot (copied from CustomerMaster at checkout)
        [MaxLength(255)]
        public string? BillingAddress { get; set; }
        [MaxLength(100)]
        public string? BillingCity { get; set; }
        [MaxLength(100)]
        public string? BillingState { get; set; }
        [MaxLength(100)]
        public string? BillingCountry { get; set; }
        [MaxLength(20)]
        public string? BillingPincode { get; set; }

        // Shipping address snapshot
        [MaxLength(255)]
        public string? ShippingAddress { get; set; }
        [MaxLength(100)]
        public string? ShippingCity { get; set; }
        [MaxLength(100)]
        public string? ShippingState { get; set; }
        [MaxLength(100)]
        public string? ShippingCountry { get; set; }
        [MaxLength(20)]
        public string? ShippingPincode { get; set; }

        [MaxLength(500)]
        public string? Notes { get; set; }

        public long? ProcessedBy { get; set; }
        public DateTime? ShippedDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public DateTime? CancelledDate { get; set; }

        public long? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public CustomerMaster? Customer { get; set; }
        public List<SalesOrderItem>? Items { get; set; }
    }
}