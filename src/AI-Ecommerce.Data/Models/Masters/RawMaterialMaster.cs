using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AI_Ecommerce.Data.Models.Masters
{
    /// <summary>Raw material used in manufacturing/assembly of finished products.</summary>
    public class RawMaterialMaster : AuditableEntity
    {
        [Key]
        public int RawMaterialId { get; set; }

        [Required]
        [MaxLength(50)]
        public string RawMaterialCode { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string RawMaterialName { get; set; } = string.Empty;

        public int? UnitId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? PurchasePrice { get; set; }

        public bool IsActive { get; set; } = true;

        public UnitMaster? Unit { get; set; }
    }
}
