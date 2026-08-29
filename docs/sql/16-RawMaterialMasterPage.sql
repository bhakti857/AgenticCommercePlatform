-- ============================================================
-- Page: Raw Material Master
-- Route: /masters/rawmaterial (planned)
-- Access: Employee account only (Add/List/Edit/Delete)
-- API: GET/POST/PUT/DELETE /api/rawmaterial-master (planned)
-- Status: PLANNED (model exists: RawMaterialMaster, no API/UI yet)
-- Tables used: RawMaterialMaster (CRUD), UnitMaster (SELECT dropdown),
--              RawMaterialStock (future)
-- ============================================================

-- ADD tab
INSERT INTO RawMaterialMaster (RawMaterialCode, RawMaterialName, UnitId,
                               PurchasePrice, IsActive, CreatedBy, CreatedAt)
VALUES (@RawMaterialCode, @RawMaterialName, @UnitId,
        @PurchasePrice, 1, @CreatedBy, SYSUTCDATETIME());

-- LIST tab
SELECT r.RawMaterialId, r.RawMaterialCode, r.RawMaterialName,
       u.UnitName, r.PurchasePrice, r.IsActive, r.CreatedAt
FROM   RawMaterialMaster r
LEFT   JOIN UnitMaster u ON u.UnitId = r.UnitId
WHERE  r.DeletedAt IS NULL
ORDER  BY r.RawMaterialCode;

-- EDIT button
UPDATE RawMaterialMaster
SET    RawMaterialCode = @RawMaterialCode,
       RawMaterialName = @RawMaterialName,
       UnitId          = @UnitId,
       PurchasePrice   = @PurchasePrice,
       IsActive        = @IsActive,
       ModifiedBy      = @ModifiedBy,
       ModifiedAt      = SYSUTCDATETIME()
WHERE  RawMaterialId = @RawMaterialId;

-- DELETE button (soft delete)
UPDATE RawMaterialMaster
SET    DeletedBy = @DeletedBy, DeletedAt = SYSUTCDATETIME()
WHERE  RawMaterialId = @RawMaterialId;