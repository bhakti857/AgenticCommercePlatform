using System.ComponentModel.DataAnnotations;

namespace AI_Ecommerce.Data.Models.Masters
{
    /// <summary>Physical/logical stock location. ProductStock is tracked per warehouse.</summary>
    public class WarehouseMaster : AuditableEntity
    {
        [Key]
        public int WarehouseId { get; set; }

        [Required]
        [MaxLength(150)]
        public string WarehouseName { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Address { get; set; }
        [MaxLength(100)]
        public string? City { get; set; }
        [MaxLength(100)]
        public string? State { get; set; }
        [MaxLength(20)]
        public string? Pincode { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
