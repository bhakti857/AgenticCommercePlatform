using System.ComponentModel.DataAnnotations;

namespace AI_Ecommerce.Data.Models.Masters
{
    /// <summary>Reference table for employee departments (e.g. CEO, Software Developer).</summary>
    public class DepartmentMaster : AuditableEntity
    {
        [Key]
        public long DepartmentId { get; set; }

        [Required]
        [MaxLength(100)]
        public string DepartmentName { get; set; } = string.Empty;
    }
}
