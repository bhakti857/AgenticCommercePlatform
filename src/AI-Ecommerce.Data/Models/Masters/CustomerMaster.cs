using System;
using System.ComponentModel.DataAnnotations;

namespace AI_Ecommerce.Data.Models.Masters
{
    /// <summary>
    /// Customer account/login table. Separate from EmployeeMaster so customer
    /// and staff auth are fully independent (different tables, different JWT
    /// claims) rather than sharing one polymorphic Users table.
    /// </summary>
    public class CustomerMaster
    {
        [Key]
        public long CustomerId { get; set; }

        public Guid UniqueId { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(255)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        // Profile / address fields
        [MaxLength(255)]
        public string? AddressLine { get; set; }
        [MaxLength(100)]
        public string? City { get; set; }
        [MaxLength(100)]
        public string? State { get; set; }
        [MaxLength(100)]
        public string? Country { get; set; }
        [MaxLength(20)]
        public string? Pincode { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
