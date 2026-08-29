using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AI_Ecommerce.Data.Models.Masters;

namespace AI_Ecommerce.Data.Models.Transactions
{
    /// <summary>Purchase order header — ordered from a vendor for products and/or raw materials.</summary>
    public class PurchaseOrder
    {
        [Key]
        public long PurchaseOrderId { get; set; }

        [Required]
        [MaxLength(50)]
        public string PurchaseOrderNo { get; set; } = string.Empty;

        [Required]
        public int VendorId { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "decimal(18,2)")]
        public decimal SubTotal { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TaxAmount { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        /// <summary>Draft → Sent → Received → Completed (or Cancelled).</summary>
        [Required]
        [MaxLength(30)]
        public string OrderStatus { get; set; } = "Draft";

        [MaxLength(500)]
        public string? Notes { get; set; }

        public long? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public VendorMaster? Vendor { get; set; }
        public List<PurchaseOrderItem>? Items { get; set; }
    }
}