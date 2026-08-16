using System.ComponentModel.DataAnnotations;

namespace AI_Ecommerce.Data.Models.Masters
{
    /// <summary>Unit of measure reference table (e.g. Piece, Kg, Litre, Box).</summary>
    public class UnitMaster : AuditableEntity
    {
        [Key]
        public int UnitId { get; set; }

        [Required]
        [MaxLength(50)]
        public string UnitName { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
    }
}
