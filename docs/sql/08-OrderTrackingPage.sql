-- ============================================================
-- Page: Order Tracking
-- Route: /track-order (planned)
-- Access: Customer account only (sees only their own orders)
-- API: GET /api/orders, GET /api/orders/{id} (read-only part already built)
-- Status: PLANNED (UI page; read queries already exist in OrdersController)
-- Tables used: Orders (SELECT), OrderItems (SELECT)
-- ============================================================

-- 1. List the customer's orders, newest first
SELECT Id, OrderNumber, SubTotal, TaxAmount, ShippingCost,
       DiscountAmount, TotalAmount, OrderStatus, PaymentStatus,
       OrderDate, ShippedDate, DeliveredDate, CancelledDate, CreatedAt
FROM   Orders
WHERE  CustomerId = @CustomerId
ORDER  BY CreatedAt DESC;

-- 2. Expand a single order into its line items
SELECT o.OrderNumber, o.OrderStatus, o.PaymentStatus, o.TotalAmount,
       o.ShippedDate, o.DeliveredDate, o.CancelledDate,
       i.ProductName, i.ProductSKU, i.Quantity, i.UnitPrice, i.TotalPrice
FROM   Orders o
JOIN   OrderItems i ON i.OrderId = o.Id
WHERE  o.Id = @OrderId AND o.CustomerId = @CustomerId;