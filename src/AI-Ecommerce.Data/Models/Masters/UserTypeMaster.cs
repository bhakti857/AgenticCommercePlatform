using System.ComponentModel.DataAnnotations;

namespace AI_Ecommerce.Data.Models.Masters
{
    /// <summary>
    /// Reference table for employee roles: 1 MasterAdmin, 2 Admin, 3 Senior,
    /// 4 Junior, 5 User. Replaces the old flat UserType enum-like table for
    /// the new Employee/Customer split (customers no longer have a UserType —
    /// they are simply rows in CustomerMaster).
    /// </summary>
    public class UserTypeMaster : AuditableEntity
    {
        [Key]
        public long UserTypeId { get; set; }

        [Required]
        [MaxLength(100)]
        public string UserTypeName { get; set; } = string.Empty;
    }
}
