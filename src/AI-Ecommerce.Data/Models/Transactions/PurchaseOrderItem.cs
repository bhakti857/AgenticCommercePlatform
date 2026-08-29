using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AI_Ecommerce.Data.Models.Transactions
{
    /// <summary>
    /// Line item on a purchase order. One row covers either a finished
    /// ProductMaster ("Product") or a RawMaterialMaster ("RawMaterial"),
    /// selected via ItemType. ItemCode/ItemName are snapshotted at order time.
    /// </summary>
    public class PurchaseOrderItem
    {
        [Key]
        public long PurchaseOrderItemId { get; set; }

        [Required]
        public long PurchaseOrderId { get; set; }

        /// <summary>"Product" or "RawMaterial"</summary>
        [Required]
        [MaxLength(20)]
        public string ItemType { get; set; } = "Product";

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

        [MaxLength(50)]
        public string ItemCode { get; set; } = string.Empty;

        [MaxLength(200)]
        public string ItemName { get; set; } = string.Empty;

        public PurchaseOrder? PurchaseOrder { get; set; }
    }
}