using AI_Ecommerce.Data.Models.Masters;
using System.ComponentModel.DataAnnotations;

namespace AI_Ecommerce.Data.Models
{
    /// <summary>
    /// Long-lived token used to mint fresh JWTs without requiring the user to
    /// re-enter their password. Only the SHA-256 hash of the token is stored —
    /// the raw token is handed to the client exactly once at issue time. Tokens
    /// are rotated on every use (the old row is revoked and a new one issued)
    /// and can be revoked explicitly at logout via <see cref="RevokedAt"/>.
    /// </summary>
    public class RefreshToken
    {
        [Key]
        public long Id { get; set; }

        /// <summary>SHA-256 hash of the raw token (random 64 bytes, base64).</summary>
        [Required]
        [MaxLength(128)]
        public string TokenHash { get; set; } = string.Empty;

        /// <summary>Set for customer account tokens; null for employee tokens.</summary>
        public long? CustomerId { get; set; }

        /// <summary>Set for employee account tokens; null for customer tokens.</summary>
        public long? EmployeeId { get; set; }

        public DateTime IssuedAt { get; set; } = DateTime.UtcNow;

        public DateTime ExpiresAt { get; set; }

        public DateTime? RevokedAt { get; set; }

        public CustomerMaster? Customer { get; set; }

        public EmployeeMaster? Employee { get; set; }
    }
}