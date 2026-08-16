using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AI_Ecommerce.Data;
using AI_Ecommerce.Data.Models;
using System.Security.Claims;

namespace AI_Ecommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public OrdersController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/orders
    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var userId = GetUserId();
        var orders = await _context.Orders
            .Include(o => o.OrderItems)
            .Where(o => o.CustomerId == userId)
            .OrderByDescending(o => o.CreatedAt)
            .Select(o => new OrderDto
            {
                Id = o.Id,
                OrderNumber = o.OrderNumber,
                TotalAmount = o.TotalAmount,
                OrderStatus = o.OrderStatus,
                CreatedAt = o.CreatedAt,
                Items = o.OrderItems.Select(i => new OrderItemDto
                {
                    Id = i.Id,
                    ProductName = i.ProductName,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    TotalPrice = i.TotalPrice
                }).ToList()
            })
            .ToListAsync();
        return Ok(orders);
    }

    // GET: api/orders/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrder(Guid id)
    {
        var userId = GetUserId();
        var order = await _context.Orders
            .Include(o => o.OrderItems)
            .Where(o => o.Id == id && o.CustomerId == userId)
            .Select(o => new OrderDto
            {
                Id = o.Id,
                OrderNumber = o.OrderNumber,
                TotalAmount = o.TotalAmount,
                OrderStatus = o.OrderStatus,
                CreatedAt = o.CreatedAt,
                Items = o.OrderItems.Select(i => new OrderItemDto
                {
                    Id = i.Id,
                    ProductName = i.ProductName,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    TotalPrice = i.TotalPrice
                }).ToList()
            })
            .FirstOrDefaultAsync();

        if (order == null) return NotFound();
        return Ok(order);
    }

    // POST: api/orders
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
    {
        var userId = GetUserId();

        if (request.Items == null || request.Items.Count == 0)
            return BadRequest("Order must contain at least one item.");

        // Wrap validation + stock deduction + order creation in a single
        // transaction, and deduct stock with an atomic conditional UPDATE
        // (StockQuantity >= quantity) so concurrent requests for the last
        // unit of a product can't both succeed and oversell it.
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            decimal subTotal = 0;
            var orderItems = new List<OrderItem>();

            foreach (var item in request.Items)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product == null)
                    return BadRequest($"Product {item.ProductId} not found");

                if (item.Quantity <= 0)
                    return BadRequest($"Invalid quantity for product {item.ProductId}");

                var rowsAffected = await _context.Database.ExecuteSqlInterpolatedAsync(
                    $"UPDATE Products SET StockQuantity = StockQuantity - {item.Quantity} WHERE Id = {item.ProductId} AND StockQuantity >= {item.Quantity}");

                if (rowsAffected == 0)
                {
                    await transaction.RollbackAsync();
                    return BadRequest($"Insufficient stock for {product.Name}");
                }

                var unitPrice = product.Price;
                var totalPrice = unitPrice * item.Quantity;
                subTotal += totalPrice;

                orderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = unitPrice,
                    TotalPrice = totalPrice,
                    ProductSKU = product.SKU,
                    ProductName = product.Name
                });
            }

            var tax = subTotal * 0.10m; // 10% tax
            var shipping = subTotal > 100 ? 0 : 10;
            var total = subTotal + tax + shipping;

            var order = new Order
            {
                Id = Guid.NewGuid(),
                OrderNumber = $"ORD-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 8)}",
                CustomerId = userId,
                SubTotal = subTotal,
                TaxAmount = tax,
                ShippingCost = shipping,
                TotalAmount = total,
                OrderStatus = "Pending",
                PaymentStatus = "Pending",
                OrderItems = orderItems,
                CreatedAt = DateTime.UtcNow
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            // Return DTO
            var orderDto = new OrderDto
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                TotalAmount = order.TotalAmount,
                OrderStatus = order.OrderStatus,
                CreatedAt = order.CreatedAt,
                Items = order.OrderItems.Select(i => new OrderItemDto
                {
                    Id = i.Id,
                    ProductName = i.ProductName,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    TotalPrice = i.TotalPrice
                }).ToList()
            };

            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, orderDto);
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    // Helper to get current customer ID from JWT (orders belong to customers only)
    private long GetUserId()
    {
        var accountType = User.FindFirst("AccountType")?.Value;
        if (accountType != "Customer")
            throw new UnauthorizedAccessException("Only customer accounts can place/view orders.");

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                       ?? User.FindFirst("sub")?.Value;
        return long.Parse(userIdClaim!);
    }
}

// ============================================================
// 📦 DTOs (Data Transfer Objects) - Put them here
// ============================================================

public class OrderDto
{
    public Guid Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public string OrderStatus { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public List<OrderItemDto> Items { get; set; } = new();
}

public class OrderItemDto
{
    public int Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
}

public class CreateOrderRequest
{
    public List<OrderItemRequest> Items { get; set; } = new();
}

public class OrderItemRequest
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}