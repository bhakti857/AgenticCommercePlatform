using AI_Ecommerce.Data;
using AI_Ecommerce.Data.Models.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AI_Ecommerce.Api.Controllers
{
    /// <summary>
    /// Sales order tracking + lifecycle. Customers see and track their own
    /// orders; employees can advance the status (Placed → Processing →
    /// Shipped → Delivered, or Cancelled) and list all orders.
    /// </summary>
    [ApiController]
    [Route("api/sales-orders")]
    [Authorize]
    public class SalesOrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SalesOrdersController(ApplicationDbContext context) => _context = context;

        private bool IsEmployee() => User.FindFirst("AccountType")?.Value == "Employee";
        private bool IsCustomer() => User.FindFirst("AccountType")?.Value == "Customer";

        private long? CurrentId =>
            long.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var id) ? id : null;

        private static object ToDto(SalesOrder o) => new
        {
            o.SalesOrderId,
            o.SalesOrderNo,
            o.OrderDate,
            o.SubTotal,
            o.TaxAmount,
            o.ShippingCost,
            o.DiscountAmount,
            o.TotalAmount,
            o.PaymentMethod,
            o.PaymentStatus,
            o.OrderStatus,
            o.BillingAddress,
            o.BillingCity,
            o.BillingState,
            o.BillingCountry,
            o.BillingPincode,
            o.ShippingAddress,
            o.ShippingCity,
            o.ShippingState,
            o.ShippingCountry,
            o.ShippingPincode,
            o.ShippedDate,
            o.DeliveredDate,
            o.CancelledDate,
            o.CreatedAt,
            Items = o.Items!.Select(i => new
            {
                i.SalesOrderItemId,
                i.ProductId,
                i.ProductCode,
                i.ProductName,
                i.Quantity,
                i.UnitPrice,
                i.DiscountAmount,
                i.TotalPrice
            })
        };

        // Customer: my orders (tracking)
        [HttpGet]
        public async Task<IActionResult> GetMyOrders()
        {
            if (!IsCustomer()) return Forbid();
            var id = CurrentId;
            var orders = await _context.SalesOrders
                .Include(o => o.Items)
                .Where(o => o.CustomerId == id)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
            return Ok(orders.Select(ToDto));
        }

        // Employee: all orders
        [HttpGet("all")]
        public async Task<IActionResult> GetAllOrders()
        {
            if (!IsEmployee()) return Forbid();
            var orders = await _context.SalesOrders
                .Include(o => o.Items)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
            return Ok(orders.Select(ToDto));
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetOrder(long id)
        {
            IQueryable<SalesOrder> query = _context.SalesOrders.Include(o => o.Items);
            if (IsCustomer()) query = query.Where(o => o.CustomerId == CurrentId);

            var order = await query.FirstOrDefaultAsync(o => o.SalesOrderId == id);
            if (order == null) return NotFound();
            return Ok(ToDto(order));
        }

        /// <summary>
        /// Advance (or cancel) an order's status. Employee-only.
        /// Valid: Placed → Processing → Shipped → Delivered; any → Cancelled.
        /// </summary>
        [HttpPatch("{id:long}/status")]
        public async Task<IActionResult> UpdateStatus(long id, UpdateOrderStatusRequest request)
        {
            if (!IsEmployee()) return Forbid();

            var order = await _context.SalesOrders.FindAsync(id);
            if (order == null) return NotFound();

            var allowed = new[] { "Placed", "Processing", "Shipped", "Delivered", "Cancelled" };
            if (!allowed.Contains(request.Status))
                return BadRequest($"Status must be one of: {string.Join(", ", allowed)}.");

            var now = DateTime.UtcNow;
            order.OrderStatus = request.Status;
            order.ProcessedBy = CurrentId;
            order.UpdatedAt = now;
            if (request.Status == "Shipped") order.ShippedDate = now;
            if (request.Status == "Delivered") order.DeliveredDate = now;
            if (request.Status == "Cancelled") order.CancelledDate = now;

            await _context.SaveChangesAsync();
            return Ok(new { order.SalesOrderId, order.OrderStatus });
        }
    }

    public class UpdateOrderStatusRequest
    {
        public string Status { get; set; } = string.Empty;
    }
}