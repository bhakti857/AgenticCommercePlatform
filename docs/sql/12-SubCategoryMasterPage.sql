-- ============================================================
-- Page: Sub-Category Master
-- Route: /masters/subcategory (planned)
-- Access: Employee account only (Add/List/Edit/Delete)
-- API: GET/POST/PUT/DELETE /api/subcategory-master (planned)
-- Status: PLANNED (model exists: SubCategoryMaster, no API/UI yet)
-- Tables used: SubCategoryMaster (CRUD), CategoryMaster (SELECT dropdown),
--              ProductMaster (count check on delete)
-- ============================================================

-- ADD tab (CategoryId is required)
INSERT INTO SubCategoryMaster (CategoryId, SubCategoryName, IsActive, CreatedBy, CreatedAt)
VALUES (@CategoryId, @SubCategoryName, 1, @CreatedBy, SYSUTCDATETIME());

-- LIST tab
SELECT s.SubCategoryId, s.SubCategoryName, c.CategoryName,
       s.IsActive, s.CreatedAt
FROM   SubCategoryMaster s
JOIN   CategoryMaster c ON c.CategoryId = s.CategoryId
WHERE  s.DeletedAt IS NULL
ORDER  BY c.CategoryName, s.SubCategoryName;

-- EDIT button
UPDATE SubCategoryMaster
SET    CategoryId      = @CategoryId,
       SubCategoryName = @SubCategoryName,
       IsActive        = @IsActive,
       ModifiedBy      = @ModifiedBy,
       ModifiedAt      = SYSUTCDATETIME()
WHERE  SubCategoryId = @SubCategoryId;

-- DELETE button (soft delete)
UPDATE SubCategoryMaster
SET    DeletedBy = @DeletedBy, DeletedAt = SYSUTCDATETIME()
WHERE  SubCategoryId = @SubCategoryId;