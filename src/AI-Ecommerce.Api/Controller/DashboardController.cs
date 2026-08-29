using AI_Ecommerce.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AI_Ecommerce.Api.Controllers
{
    /// <summary>Employee dashboard summary — aggregates across the master/transaction/inventory tables.</summary>
    [ApiController]
    [Route("api/dashboard")]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context) => _context = context;

        private bool IsEmployee() => User.FindFirst("AccountType")?.Value == "Employee";

        [HttpGet("summary")]
        public async Task<IActionResult> Summary()
        {
            if (!IsEmployee()) return Forbid();

            var counts = new
            {
                Customers = await _context.CustomerMasters.CountAsync(),
                Employees = await _context.EmployeeMasters.CountAsync(),
                Products = await _context.ProductMasters.CountAsync(),
                Vendors = await _context.VendorMasters.CountAsync(),
                Warehouses = await _context.WarehouseMasters.CountAsync(),
                RawMaterials = await _context.RawMaterialMasters.CountAsync(),
                Categories = await _context.CategoryMasters.CountAsync(),
                Departments = await _context.DepartmentMasters.CountAsync(),
                OpenOrders = await _context.SalesOrders.CountAsync(o => o.OrderStatus != "Delivered" && o.OrderStatus != "Cancelled"),
                PendingPayments = await _context.Payments.CountAsync(p => p.Status == "Pending")
            };

            var ordersByStatus = await _context.SalesOrders
                .GroupBy(o => o.OrderStatus)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToListAsync();

            var recentOrders = await _context.SalesOrders
                .OrderByDescending(o => o.CreatedAt)
                .Take(5)
                .Select(o => new
                {
                    o.SalesOrderNo,
                    o.TotalAmount,
                    o.PaymentMethod,
                    o.OrderStatus,
                    o.CreatedAt
                })
                .ToListAsync();

            var lowStock = (await _context.ProductStocks
                .Include(s => s.Product)
                .ToListAsync())
                .GroupBy(s => s.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    ProductName = g.First().Product!.ProductName,
                    Available = g.Sum(s => s.Quantity - s.ReservedQuantity)
                })
                .Where(x => x.Available <= 10)
                .OrderBy(x => x.Available)
                .Take(10)
                .ToList();

            var pendingApprovals = await _context.ProductMasters
                .Where(p => p.Approval3At == null)
                .Take(10)
                .Select(p => new
                {
                    p.ProductCode,
                    p.ProductName,
                    Approval1At = p.Approval1At != null,
                    Approval2At = p.Approval2At != null,
                    Approval3At = p.Approval3At != null
                })
                .ToListAsync();

            return Ok(new
            {
                counts,
                ordersByStatus,
                recentOrders,
                lowStock,
                pendingApprovals
            });
        }
    }
}