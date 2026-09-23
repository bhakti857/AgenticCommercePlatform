using AI_Ecommerce.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AI_Ecommerce.Api.Controllers
{
    /// <summary>
    /// Read-only view over the login-audit tables (one row per successful
    /// login). Employee-only; exposes browser/OS/IP for each login with the
    /// owning account's name, most recent first.
    /// </summary>
    [ApiController]
    [Route("api/audit")]
    [Authorize(Policy = "RequireEmployee")]
    public class AuditController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuditController(ApplicationDbContext context) => _context = context;

        /// <summary>Recent employee logins.</summary>
        [HttpGet("employee-logs")]
        public async Task<IActionResult> GetEmployeeLogs([FromQuery] int? page, [FromQuery] int? pageSize)
        {
            IQueryable<object> query = _context.EmployeeLogs
                .AsNoTracking()
                .OrderByDescending(l => l.LogDateTime)
                .Select(l => new
                {
                    l.LogId,
                    l.EmployeeId,
                    Name = l.Employee != null ? l.Employee.FirstName + " " + l.Employee.LastName : "",
                    l.IPAddress,
                    l.OSFamily,
                    l.OSVersion,
                    l.BrowserFamily,
                    l.BrowserVersion,
                    l.LogDateTime
                });

            var result = await Services.PaginationHelper.ToResultAsync(query, page, pageSize, HttpContext.RequestAborted);
            return Ok(result);
        }

        /// <summary>Recent customer logins.</summary>
        [HttpGet("customer-logs")]
        public async Task<IActionResult> GetCustomerLogs([FromQuery] int? page, [FromQuery] int? pageSize)
        {
            IQueryable<object> query = _context.CustomerLogs
                .AsNoTracking()
                .OrderByDescending(l => l.LogDateTime)
                .Select(l => new
                {
                    l.LogId,
                    l.CustomerId,
                    Name = l.Customer != null ? l.Customer.FirstName + " " + l.Customer.LastName : "",
                    l.IPAddress,
                    l.OSFamily,
                    l.OSVersion,
                    l.BrowserFamily,
                    l.BrowserVersion,
                    l.LogDateTime
                });

            var result = await Services.PaginationHelper.ToResultAsync(query, page, pageSize, HttpContext.RequestAborted);
            return Ok(result);
        }
    }
}