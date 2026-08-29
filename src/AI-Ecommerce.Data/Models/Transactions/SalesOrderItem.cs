using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AI_Ecommerce.Data.Models.Masters;

namespace AI_Ecommerce.Data.Models.Transactions
{
    /// <summary>Line item on a sales order — snapshots the product code/name and price at sale time.</summary>
    public class SalesOrderItem
    {
        [Key]
        public long SalesOrderItemId { get; set; }

        [Required]
        public long SalesOrderId { get; set; }

        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountAmount { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        [MaxLength(50)]
        public string ProductCode { get; set; } = string.Empty;

        [MaxLength(200)]
        public string ProductName { get; set; } = string.Empty;

        public SalesOrder? SalesOrder { get; set; }
        public ProductMaster? Product { get; set; }
    }
}