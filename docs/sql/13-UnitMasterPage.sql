-- ============================================================
-- Page: Unit Master
-- Route: /masters/unit (planned)
-- Access: Employee account only (Add/List/Edit/Delete)
-- API: GET/POST/PUT/DELETE /api/unit-master (planned)
-- Status: PLANNED (model exists: UnitMaster, no API/UI yet)
-- Tables used: UnitMaster (CRUD), ProductMaster / RawMaterialMaster
--              (count check on delete)
-- ============================================================

-- ADD tab
INSERT INTO UnitMaster (UnitName, IsActive, CreatedBy, CreatedAt)
VALUES (@UnitName, 1, @CreatedBy, SYSUTCDATETIME());

-- LIST tab
SELECT UnitId, UnitName, IsActive, CreatedBy, CreatedAt, ModifiedAt
FROM   UnitMaster
WHERE  DeletedAt IS NULL
ORDER  BY UnitName;

-- EDIT button
UPDATE UnitMaster
SET    UnitName   = @UnitName,
       IsActive   = @IsActive,
       ModifiedBy = @ModifiedBy,
       ModifiedAt = SYSUTCDATETIME()
WHERE  UnitId = @UnitId;

-- DELETE button (soft delete)
UPDATE UnitMaster
SET    DeletedBy = @DeletedBy, DeletedAt = SYSUTCDATETIME()
WHERE  UnitId = @UnitId;