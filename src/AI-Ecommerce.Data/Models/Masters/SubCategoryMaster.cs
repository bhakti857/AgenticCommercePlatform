using System.ComponentModel.DataAnnotations;

namespace AI_Ecommerce.Data.Models.Masters
{
    public class SubCategoryMaster : AuditableEntity
    {
        [Key]
        public int SubCategoryId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(150)]
        public string SubCategoryName { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public CategoryMaster? Category { get; set; }
    }
}
