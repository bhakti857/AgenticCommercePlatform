using System.ComponentModel.DataAnnotations;

namespace AI_Ecommerce.Data.Models.Masters
{
    public class CategoryMaster : AuditableEntity
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(150)]
        public string CategoryName { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
    }
}
