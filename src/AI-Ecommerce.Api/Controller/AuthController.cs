using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using AI_Ecommerce.Data;
using AI_Ecommerce.Data.Models.Masters;
using AI_Ecommerce.Data.Utils;
using AI_Ecommerce.Api.Services;

namespace AI_Ecommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[EnableRateLimiting("auth")]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly JwtService _jwtService;

    public AuthController(ApplicationDbContext context, JwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    public class RegisterRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
    }

    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class AuthResponse
    {
        public string Token { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string AccountType { get; set; } = string.Empty; // "Customer" or "Employee"
        public long? UserTypeId { get; set; } // employees only
    }

    public class RegisterEmployeeRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public long DepartmentId { get; set; }

        /// <summary>UserTypeMaster id: 1 MasterAdmin, 2 Admin, 3 Senior, 4 Junior, 5 User. Defaults to User (5).</summary>
        public long UserTypeId { get; set; } = 5;
    }

    /// <summary>
    /// Public self-registration — always creates a CustomerMaster row. Employees
    /// can never be created here; see POST /api/auth/register-employee.
    /// </summary>
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        if (await _context.CustomerMasters.AnyAsync(c => c.Email == request.Email))
            return BadRequest("Email already exists.");

        var customer = new CustomerMaster
        {
            Email = request.Email,
            PasswordHash = PasswordHasher.HashPassword(request.Password),
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            IsActive = true
        };

        _context.CustomerMasters.Add(customer);
        await _context.SaveChangesAsync();

        var token = _jwtService.GenerateToken(customer.CustomerId, customer.Email, "Customer", null);

        return Ok(new AuthResponse
        {
            Token = token,
            Email = customer.Email,
            FullName = $"{customer.FirstName} {customer.LastName}",
            AccountType = "Customer",
            UserTypeId = null
        });
    }

    /// <summary>
    /// Creates a staff account (EmployeeMaster). Restricted to callers who are
    /// already MasterAdmin (1) or Admin (2). A caller can only create accounts at
    /// their own UserTypeId level or lower-privileged ones (i.e. numerically
    /// greater-or-equal) — this closes the privilege-escalation gap where an
    /// Admin (2) could previously mint a new MasterAdmin (1) account. Only a
    /// MasterAdmin may create another MasterAdmin.
    /// </summary>
    [HttpPost("register-employee")]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public async Task<IActionResult> RegisterEmployee(RegisterEmployeeRequest request)
    {
        var accountType = User.FindFirst("AccountType")?.Value;
        var callerUserTypeClaim = User.FindFirst("UserTypeId")?.Value;
        if (accountType != "Employee" || callerUserTypeClaim == null ||
            !long.TryParse(callerUserTypeClaim, out var callerUserTypeId) ||
            (callerUserTypeId != 1 && callerUserTypeId != 2))
        {
            return Forbid();
        }

        if (!await _context.UserTypeMasters.AnyAsync(t => t.UserTypeId == request.UserTypeId))
            return BadRequest("Invalid UserTypeId.");

        // Only a MasterAdmin (1) may create another MasterAdmin. An Admin (2) may
        // only create UserTypeId >= their own (2, 3, 4, 5) — never a more
        // privileged account than themselves.
        if (request.UserTypeId < callerUserTypeId)
            return Forbid();

        if (!await _context.DepartmentMasters.AnyAsync(d => d.DepartmentId == request.DepartmentId))
            return BadRequest("Invalid DepartmentId.");

        if (await _context.EmployeeMasters.AnyAsync(e => e.Email == request.Email))
            return BadRequest("Email already exists.");

        var callerId = long.Parse(User.FindFirst("sub")!.Value);

        var employee = new EmployeeMaster
        {
            Email = request.Email,
            PasswordHash = PasswordHasher.HashPassword(request.Password),
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            DepartmentId = request.DepartmentId,
            UserTypeId = request.UserTypeId,
            CreatedBy = callerId,
            IsActive = true
        };

        _context.EmployeeMasters.Add(employee);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            employee.Email,
            FullName = $"{employee.FirstName} {employee.LastName}",
            employee.UserTypeId
        });
    }

    /// <summary>
    /// Single login endpoint for both account types: tries CustomerMaster first,
    /// then EmployeeMaster. Emails are unique within each table but the two
    /// tables are independent, so in the rare case the same email exists in both
    /// (not possible via normal registration flows) the customer record wins.
    /// </summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var customer = await _context.CustomerMasters.FirstOrDefaultAsync(c => c.Email == request.Email);
        if (customer != null)
        {
            if (!PasswordHasher.VerifyPassword(request.Password, customer.PasswordHash))
                return Unauthorized("Invalid email or password.");

            var token = _jwtService.GenerateToken(customer.CustomerId, customer.Email, "Customer", null);
            await Services.LoginAudit.RecordAsync(_context, HttpContext, customerId: customer.CustomerId, employeeId: null, token);
            return Ok(new AuthResponse
            {
                Token = token,
                Email = customer.Email,
                FullName = $"{customer.FirstName} {customer.LastName}",
                AccountType = "Customer",
                UserTypeId = null
            });
        }

        var employee = await _context.EmployeeMasters.FirstOrDefaultAsync(e => e.Email == request.Email);
        if (employee != null)
        {
            if (!PasswordHasher.VerifyPassword(request.Password, employee.PasswordHash))
                return Unauthorized("Invalid email or password.");

            var employeeToken = _jwtService.GenerateToken(employee.EmployeeId, employee.Email, "Employee", employee.UserTypeId);
            await Services.LoginAudit.RecordAsync(_context, HttpContext, customerId: null, employeeId: employee.EmployeeId, employeeToken);
            return Ok(new AuthResponse
            {
                Token = employeeToken,
                Email = employee.Email,
                FullName = $"{employee.FirstName} {employee.LastName}",
                AccountType = "Employee",
                UserTypeId = employee.UserTypeId
            });
        }

        return Unauthorized("Invalid email or password.");
    }
}
