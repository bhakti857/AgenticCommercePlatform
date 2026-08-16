using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AI_Ecommerce.Data.Models.Masters
{
    /// <summary>Supplier reference table, used by PurchaseOrder.</summary>
    public class VendorMaster : AuditableEntity
    {
        [Key]
        public int VendorId { get; set; }

        [Required]
        [MaxLength(200)]
        public string VendorName { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Email { get; set; }
        [MaxLength(20)]
        public string? PhoneNumber { get; set; }
        [MaxLength(255)]
        public string? Address { get; set; }
        [MaxLength(100)]
        public string? City { get; set; }
        [MaxLength(100)]
        public string? State { get; set; }
        [MaxLength(100)]
        public string? Country { get; set; }
        [MaxLength(20)]
        public string? Pincode { get; set; }
        [MaxLength(50)]
        public string? GSTNumber { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
