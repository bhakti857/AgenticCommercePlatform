-- ============================================================
-- Page: Product Master
-- Route: /masters/product (planned)
-- Access: Employee account only (Add/List/Edit/Delete)
-- API: GET/POST/PUT/DELETE /api/product-master (planned)
-- Status: PLANNED (model exists: ProductMaster, no API/UI yet)
-- Tables used: ProductMaster (CRUD), CategoryMaster / SubCategoryMaster /
--              UnitMaster (SELECT dropdowns)
-- ============================================================

-- ADD tab: insert a product. Creator's EmployeeId goes into CreatedBy;
--         the 3-level approval fields are left NULL until approved.
INSERT INTO ProductMaster (ProductCode, ProductName, CategoryId, SubCategoryId,
                           UnitId, PurchasePrice, SellingPrice, GSTPercent,
                           IsActive, CreatedBy, CreatedAt)
VALUES (@ProductCode, @ProductName, @CategoryId, @SubCategoryId,
        @UnitId, @PurchasePrice, @SellingPrice, @GSTPercent,
        1, @CreatedBy, SYSUTCDATETIME());

-- LIST tab: grid of products with dropdown lookups
SELECT p.ProductId, p.ProductCode, p.ProductName,
       c.CategoryName, s.SubCategoryName, u.UnitName,
       p.PurchasePrice, p.SellingPrice, p.GSTPercent, p.IsActive,
       p.Approval1At, p.Approval2At, p.Approval3At
FROM   ProductMaster p
LEFT   JOIN CategoryMaster c    ON c.CategoryId    = p.CategoryId
LEFT   JOIN SubCategoryMaster s ON s.SubCategoryId = p.SubCategoryId
LEFT   JOIN UnitMaster u        ON u.UnitId        = p.UnitId
WHERE  p.DeletedAt IS NULL;

-- EDIT button: update + stamp ModifiedBy/ModifiedAt
UPDATE ProductMaster
SET    ProductCode   = @ProductCode,
       ProductName   = @ProductName,
       CategoryId    = @CategoryId,
       SubCategoryId = @SubCategoryId,
       UnitId        = @UnitId,
       PurchasePrice = @PurchasePrice,
       SellingPrice  = @SellingPrice,
       GSTPercent    = @GSTPercent,
       IsActive      = @IsActive,
       ModifiedBy    = @ModifiedBy,
       ModifiedAt    = SYSUTCDATETIME()
WHERE  ProductId = @ProductId;

-- DELETE button: soft delete + stamp DeletedBy/DeletedAt
UPDATE ProductMaster
SET    DeletedBy = @DeletedBy, DeletedAt = SYSUTCDATETIME()
WHERE  ProductId = @ProductId;

-- Approval workflow (business logic, not yet in the app): approvers stamp
-- Approval1By/Approval1At .. Approval3By/Approval3At in sequence.