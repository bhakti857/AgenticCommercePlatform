-- ============================================================
-- Page: Product List (storefront browsing)
-- Route: /products
-- Access: Any authenticated user (Customer or Employee)
-- API: GET /api/products, GET /api/products/{id}
-- Status: BUILT
-- NOTE: This page reads the LEGACY Product table, not ProductMaster.
--       ProductMaster is not wired to any API/UI yet.
-- Tables used: Products (SELECT)
-- ============================================================

-- List all products
SELECT Id, SKU, Name, Description, Price, Cost, Category,
       StockQuantity, IsActive, CreatedAt, UpdatedAt
FROM   Products;

-- Single product
SELECT Id, SKU, Name, Description, Price, Cost, Category,
       StockQuantity, IsActive, CreatedAt, UpdatedAt
FROM   Products
WHERE  Id = @Id;