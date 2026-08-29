-- ============================================================
-- Page: Warehouse Master
-- Route: /masters/warehouse (planned)
-- Access: Employee account only (Add/List/Edit/Delete)
-- API: GET/POST/PUT/DELETE /api/warehouse-master (planned)
-- Status: PLANNED (model exists: WarehouseMaster, no API/UI yet)
-- Tables used: WarehouseMaster (CRUD), ProductStock (future, per-warehouse stock)
-- ============================================================

-- ADD tab
INSERT INTO WarehouseMaster (WarehouseName, Address, City, State, Pincode,
                             IsActive, CreatedBy, CreatedAt)
VALUES (@WarehouseName, @Address, @City, @State, @Pincode,
        1, @CreatedBy, SYSUTCDATETIME());

-- LIST tab
SELECT WarehouseId, WarehouseName, Address, City, State, Pincode,
       IsActive, CreatedAt
FROM   WarehouseMaster
WHERE  DeletedAt IS NULL
ORDER  BY WarehouseName;

-- EDIT button
UPDATE WarehouseMaster
SET    WarehouseName = @WarehouseName,
       Address       = @Address,
       City          = @City,
       State         = @State,
       Pincode       = @Pincode,
       IsActive      = @IsActive,
       ModifiedBy    = @ModifiedBy,
       ModifiedAt    = SYSUTCDATETIME()
WHERE  WarehouseId = @WarehouseId;

-- DELETE button (soft delete)
UPDATE WarehouseMaster
SET    DeletedBy = @DeletedBy, DeletedAt = SYSUTCDATETIME()
WHERE  WarehouseId = @WarehouseId;

-- Future: per-warehouse stock lives in ProductStock (ProductId, WarehouseId,
-- Quantity, ReservedQuantity) once the inventory tables are built.