using System.Reflection;

namespace AI_Ecommerce.Api.Services
{
    /// <summary>
    /// Shared audit-stamping helpers used by the master CRUD controllers.
    /// Sets the common audit columns by reflection so every master table
    /// (whether it inherits AuditableEntity or only carries DeletedAt) behaves
    /// consistently.
    /// </summary>
    public static class MasterAudit
    {
        public static readonly string[] AuditProps =
            { "CreatedAt", "CreatedBy", "ModifiedAt", "ModifiedBy", "UpdatedAt", "DeletedAt", "DeletedBy", "LogId" };

        public static void SetIfHas(object entity, string name, object? value)
        {
            var prop = entity.GetType().GetProperty(name);
            if (prop != null && prop.CanWrite)
                prop.SetValue(entity, value);
        }

        public static void StampCreate(object entity, long? employeeId)
        {
            SetIfHas(entity, "CreatedAt", DateTime.UtcNow);
            SetIfHas(entity, "CreatedBy", employeeId);
        }

        public static void StampUpdate(object entity, long? employeeId)
        {
            SetIfHas(entity, "ModifiedAt", DateTime.UtcNow);
            SetIfHas(entity, "UpdatedAt", DateTime.UtcNow);
            SetIfHas(entity, "ModifiedBy", employeeId);
        }

        public static void StampDelete(object entity, long? employeeId)
        {
            SetIfHas(entity, "DeletedAt", DateTime.UtcNow);
            SetIfHas(entity, "DeletedBy", employeeId);
        }

        /// <summary>Copies scalar (non-navigation, non-key, non-audit) properties from source to target.</summary>
        public static void CopyScalars(object source, object target, Type entityType, IEnumerable<string> keyNames)
        {
            foreach (var prop in entityType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!prop.CanRead || !prop.CanWrite) continue;
                if (prop.GetIndexParameters().Length > 0) continue;
                if (keyNames.Contains(prop.Name)) continue;
                if (AuditProps.Contains(prop.Name)) continue;
                if (IsNavigation(prop.PropertyType)) continue;
                prop.SetValue(target, prop.GetValue(source));
            }
        }

        public static bool IsNavigation(Type type) =>
            type.IsClass && type != typeof(string) && type != typeof(byte[]);
    }
}