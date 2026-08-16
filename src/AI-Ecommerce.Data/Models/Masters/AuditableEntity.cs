using System;

namespace AI_Ecommerce.Data.Models.Masters
{
    /// <summary>
    /// Common audit trail fields shared by every Master table: who created,
    /// modified, or (soft) deleted the row, and a pointer to the log entry
    /// that recorded the change. Inherit this instead of repeating the same
    /// six columns on every master entity.
    /// </summary>
    public abstract class AuditableEntity
    {
        public long? CreatedBy { get; set; } // EmployeeId of creator
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public long? ModifiedBy { get; set; } // EmployeeId of editor
        public DateTime? ModifiedAt { get; set; }
        public long? DeletedBy { get; set; } // EmployeeId of deletor
        public DateTime? DeletedAt { get; set; }
        public long? LogId { get; set; } // Creation/Edition/Deletion LogId
    }
}
