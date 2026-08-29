-- ============================================================
-- Page: Create Order (storefront checkout)
-- Route: /orders
-- Access: Customer account only (JWT AccountType = "Customer")
-- API: POST /api/orders, GET /api/orders, GET /api/orders/{id}
-- Status: BUILT
-- NOTE: Uses the LEGACY Order/OrderItem/Product tables. The planned
--       SalesOrder/SalesOrderItem/ProductStock/StockTransaction tables
--       are not built yet.
-- Tables used: Products (SELECT + UPDATE), Orders (INSERT + SELECT),
--              OrderItems (INSERT + SELECT)
-- ============================================================

-- 1. Validate each item's product exists
SELECT Id, SKU, Name, Price, Cost, StockQuantity
FROM   Products
WHERE  Id = @ProductId;

-- 2. Atomically deduct stock (prevents overselling under concurrency)
UPDATE Products
SET    StockQuantity = StockQuantity - @Quantity
WHERE  Id = @ProductId AND StockQuantity >= @Quantity;
--   rowsAffected == 0 -> insufficient stock, whole order rolls back

-- 3. Create the order (everything below runs inside one DB transaction,
--    see OrdersController.CreateOrder)
INSERT INTO Orders (Id, OrderNumber, CustomerId, OrderDate,
                    SubTotal, TaxAmount, ShippingCost, DiscountAmount,
                    TotalAmount, OrderStatus, PaymentStatus,
                    ProcessedBy, ShippedDate, DeliveredDate, CancelledDate,
                    CreatedAt)
VALUES (NEWID(), @OrderNumber, @CustomerId, SYSUTCDATETIME(),
        @SubTotal, @TaxAmount, @ShippingCost, 0,
        @TotalAmount, 'Pending', 'Pending',
        NULL, NULL, NULL, NULL,
        SYSUTCDATETIME());
--   OrderNumber: 'ORD-' + yyyyMMdd + '-' + first 8 chars of a GUID
--   Tax: 10% of subtotal; Shipping: 0 if subtotal > 100 else 10

-- 4. Insert one OrderItem row per ordered product
INSERT INTO OrderItems (OrderId, ProductId, Quantity, UnitPrice, TotalPrice,
                        DiscountAmount, ProductSKU, ProductName)
VALUES (@OrderId, @ProductId, @Quantity, @UnitPrice, @TotalPrice,
        0, @ProductSKU, @ProductName);

-- 5. Order list / single order (OrderDto projection)
SELECT Id, OrderNumber, TotalAmount, OrderStatus, CreatedAt
FROM   Orders
WHERE  CustomerId = @CustomerId
ORDER  BY CreatedAt DESC;

SELECT Id, OrderId, ProductId, Quantity, UnitPrice, TotalPrice,
       ProductSKU, ProductName
FROM   OrderItems
WHERE  OrderId = @OrderId;