-- ============================================================
-- Page: Employee Dashboard
-- Route: /dashboard (planned)
-- Access: Employee account only
-- API: GET /api/dashboard/summary (planned)
-- Status: PLANNED (no UI or API yet)
-- Tables used: reads + aggregates over all master and transaction tables
-- ============================================================

-- Suggested summary queries (tune to the widgets you build):

-- Active master records
SELECT 'Customers'   AS Entity, COUNT(*) AS ActiveCount FROM CustomerMaster   WHERE IsActive = 1
UNION ALL SELECT 'Employees',   COUNT(*) FROM EmployeeMaster   WHERE IsActive = 1
UNION ALL SELECT 'Products',    COUNT(*) FROM ProductMaster    WHERE IsActive = 1
UNION ALL SELECT 'Vendors',     COUNT(*) FROM VendorMaster     WHERE IsActive = 1
UNION ALL SELECT 'Warehouses',  COUNT(*) FROM WarehouseMaster  WHERE IsActive = 1
UNION ALL SELECT 'RawMaterials',COUNT(*) FROM RawMaterialMaster WHERE IsActive = 1;

-- Pending order / payment counts (legacy Order table today;
-- switch to SalesOrder once the transaction layer lands)
SELECT OrderStatus, COUNT(*) FROM Orders GROUP BY OrderStatus;
SELECT PaymentStatus, COUNT(*) FROM Orders GROUP BY PaymentStatus;

-- Recent orders
SELECT TOP (10) OrderNumber, TotalAmount, OrderStatus, CreatedAt
FROM   Orders
ORDER  BY CreatedAt DESC;

-- Products awaiting approval (ProductMaster 3-level approval workflow)
SELECT ProductCode, ProductName,
       CASE WHEN Approval1At IS NULL THEN 'Pending' ELSE 'Approved' END AS Approval1,
       CASE WHEN Approval2At IS NULL THEN 'Pending' ELSE 'Approved' END AS Approval2,
       CASE WHEN Approval3At IS NULL THEN 'Pending' ELSE 'Approved' END AS Approval3
FROM   ProductMaster
WHERE  IsActive = 1;