using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AI_Ecommerce.Data.Models.Masters
{
    public class ProductMaster : AuditableEntity
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [MaxLength(50)]
        public string ProductCode { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string ProductName { get; set; } = string.Empty;

        public int? CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public int? UnitId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? PurchasePrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? SellingPrice { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal? GSTPercent { get; set; }

        public bool IsActive { get; set; } = true;

        // Multi-level approval workflow — a product only becomes sellable once
        // the required approvers have signed off (business rule enforced in
        // the service layer, not the database).
        public long? Approval1By { get; set; }
        public DateTime? Approval1At { get; set; }
        public long? Approval2By { get; set; }
        public DateTime? Approval2At { get; set; }
        public long? Approval3By { get; set; }
        public DateTime? Approval3At { get; set; }

        public CategoryMaster? Category { get; set; }
        public SubCategoryMaster? SubCategory { get; set; }
        public UnitMaster? Unit { get; set; }
    }
}
