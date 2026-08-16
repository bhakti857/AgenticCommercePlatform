using System;
using System.ComponentModel.DataAnnotations;

namespace AI_Ecommerce.Data.Models.Masters
{
    /// <summary>
    /// Staff account/login table (the only users who can access the employee
    /// dashboard and the AI agent). Separate from CustomerMaster.
    /// </summary>
    public class EmployeeMaster
    {
        [Key]
        public long EmployeeId { get; set; }

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

        [Required]
        public long DepartmentId { get; set; }

        [Required]
        public long UserTypeId { get; set; }

        public bool IsActive { get; set; } = true;

        public long? CreatedBy { get; set; } // EmployeeId of creator
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public DepartmentMaster? Department { get; set; }
        public UserTypeMaster? UserType { get; set; }
    }
}
