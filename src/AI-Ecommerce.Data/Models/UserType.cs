using System.ComponentModel.DataAnnotations;

namespace AI_Ecommerce.Data.Models
{
    /// <summary>
    /// Lookup table for the numeric <see cref="User.UserType"/> codes so the
    /// meaning of 1/2/3/4 is stored in the database instead of only in code
    /// comments. Rows are seeded via migration (see OnModelCreating HasData)
    /// and are not expected to change at runtime.
    /// </summary>
    public class UserType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Description { get; set; }
    }
}
