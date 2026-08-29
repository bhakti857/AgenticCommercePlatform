using AI_Ecommerce.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Security.Claims;

namespace AI_Ecommerce.Api.Controllers.Abstractions
{
    /// <summary>
    /// Generic CRUD base for the master/reference tables. Provides list /
    /// get / create / update / soft-delete using the entity's own key and the
    /// common audit columns (CreatedAt/CreatedBy/ModifiedAt/ModifiedBy/
    /// DeletedAt/DeletedBy). Soft-deleted rows are invisible everywhere because
    /// the DbContext applies a global query filter on DeletedAt.
    /// Derived classes only need to declare their route/attributes.
    /// </summary>
    public abstract class MasterCrudControllerBase<TEntity> : ControllerBase where TEntity : class
    {
        protected readonly ApplicationDbContext _context;

        protected MasterCrudControllerBase(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>EmployeeId from the JWT, or null for non-employee callers.</summary>
        protected long? CurrentEmployeeId
        {
            get
            {
                if (User.FindFirst("AccountType")?.Value != "Employee") return null;
                var sub = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return long.TryParse(sub, out var id) ? id : null;
            }
        }

        private static readonly string[] AuditProps =
            { "CreatedAt", "CreatedBy", "ModifiedAt", "ModifiedBy", "UpdatedAt", "DeletedAt", "DeletedBy", "LogId" };

        private Type KeyType =>
            _context.Model.FindEntityType(typeof(TEntity)).FindPrimaryKey().Properties[0].ClrType;

        private static void SetIfHas(object entity, string name, object? value)
        {
            var prop = entity.GetType().GetProperty(name);
            if (prop != null && prop.CanWrite)
                prop.SetValue(entity, value);
        }

        protected void StampCreate(TEntity entity)
        {
            SetIfHas(entity, "CreatedAt", DateTime.UtcNow);
            SetIfHas(entity, "CreatedBy", CurrentEmployeeId);
        }

        protected void StampUpdate(TEntity entity)
        {
            SetIfHas(entity, "ModifiedAt", DateTime.UtcNow);
            SetIfHas(entity, "UpdatedAt", DateTime.UtcNow);
            SetIfHas(entity, "ModifiedBy", CurrentEmployeeId);
        }

        protected void StampDelete(TEntity entity)
        {
            SetIfHas(entity, "DeletedAt", DateTime.UtcNow);
            SetIfHas(entity, "DeletedBy", CurrentEmployeeId);
        }

        /// <summary>Copies scalar (non-navigation, non-key, non-audit) properties from source to target.</summary>
        protected void CopyScalars(TEntity source, TEntity target)
        {
            var keyNames = _context.Model.FindEntityType(typeof(TEntity))
                .FindPrimaryKey().Properties.Select(k => k.Name).ToHashSet();

            foreach (var prop in typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!prop.CanRead || !prop.CanWrite) continue;
                if (prop.GetIndexParameters().Length > 0) continue;
                if (keyNames.Contains(prop.Name)) continue;
                if (AuditProps.Contains(prop.Name)) continue;
                if (IsNavigation(prop.PropertyType)) continue;

                prop.SetValue(target, prop.GetValue(source));
            }
        }

        private static bool IsNavigation(Type type) =>
            type.IsClass && type != typeof(string) && type != typeof(byte[]);

        protected object ParseKey(string id)
        {
            var t = KeyType;
            if (t == typeof(int)) return int.Parse(id);
            if (t == typeof(long)) return long.Parse(id);
            if (t == typeof(Guid)) return Guid.Parse(id);
            if (t == typeof(string)) return id;
            return Convert.ChangeType(id, t);
        }

        private async Task<TEntity?> FindAsync(object key) =>
            await _context.Set<TEntity>().FindAsync(key);

        [HttpGet]
        public virtual async Task<IActionResult> GetAll()
        {
            var rows = await _context.Set<TEntity>().AsNoTracking().ToListAsync();
            return Ok(rows);
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetById(string id)
        {
            var entity = await FindAsync(ParseKey(id));
            return entity == null ? NotFound() : Ok(entity);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create(TEntity entity)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            StampCreate(entity);
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Update(string id, TEntity entity)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var existing = await FindAsync(ParseKey(id));
            if (existing == null) return NotFound();
            CopyScalars(entity, existing);
            StampUpdate(existing);
            await _context.SaveChangesAsync();
            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(string id)
        {
            var existing = await FindAsync(ParseKey(id));
            if (existing == null) return NotFound();
            StampDelete(existing);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}