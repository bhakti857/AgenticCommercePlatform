-- ============================================================
-- Page: Category Master
-- Route: /masters/category (planned)
-- Access: Employee account only (Add/List/Edit/Delete)
-- API: GET/POST/PUT/DELETE /api/category-master (planned)
-- Status: PLANNED (model exists: CategoryMaster, no API/UI yet)
-- Tables used: CategoryMaster (CRUD), SubCategoryMaster (count check on delete)
-- ============================================================

-- ADD tab
INSERT INTO CategoryMaster (CategoryName, IsActive, CreatedBy, CreatedAt)
VALUES (@CategoryName, 1, @CreatedBy, SYSUTCDATETIME());

-- LIST tab
SELECT CategoryId, CategoryName, IsActive, CreatedBy, CreatedAt, ModifiedAt
FROM   CategoryMaster
WHERE  DeletedAt IS NULL
ORDER  BY CategoryName;

-- EDIT button
UPDATE CategoryMaster
SET    CategoryName = @CategoryName,
       IsActive     = @IsActive,
       ModifiedBy   = @ModifiedBy,
       ModifiedAt   = SYSUTCDATETIME()
WHERE  CategoryId = @CategoryId;

-- DELETE button (soft delete)
UPDATE CategoryMaster
SET    DeletedBy = @DeletedBy, DeletedAt = SYSUTCDATETIME()
WHERE  CategoryId = @CategoryId;

-- Optional guard before delete: categories in use
SELECT COUNT(*) FROM SubCategoryMaster WHERE CategoryId = @CategoryId AND DeletedAt IS NULL;