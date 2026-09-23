using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using AI_Ecommerce.Data;
using AI_Ecommerce.Data.Models;
using AI_Ecommerce.Data.Models.Masters;
using AI_Ecommerce.Data.Utils;
using AI_Ecommerce.Api.Services;

namespace AI_Ecommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[EnableRateLimiting("auth")]
public class AuthController : ControllerBase
{
    private static readonly TimeSpan RefreshLifetime = TimeSpan.FromDays(30);

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
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpiresAt { get; set; }
    }

    public class RefreshRequest
    {
        public string RefreshToken { get; set; } = string.Empty;
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
        request.Email = request.Email.Trim();
        if (!IsValidEmail(request.Email))
            return BadRequest("A valid email address is required.");

        var passwordError = ValidatePassword(request.Password);
        if (passwordError != null)
            return BadRequest(passwordError);

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
        var refresh = await IssueRefreshTokenAsync(customer.CustomerId, null);

        return Ok(new AuthResponse
        {
            Token = token,
            Email = customer.Email,
            FullName = $"{customer.FirstName} {customer.LastName}",
            AccountType = "Customer",
            UserTypeId = null,
            RefreshToken = refresh.Token,
            RefreshTokenExpiresAt = refresh.ExpiresAt
        });
    }

    /// <summary>
    /// Creates a staff account (EmployeeMaster). Restricted to callers who are
    /// already MasterAdmin (1) or Admin (2) — enforced by the
    /// <c>MasterAdminOrAdmin</c> authorization policy. A caller can only create
    /// accounts at their own UserTypeId level or lower-privileged ones (i.e.
    /// numerically greater-or-equal) — this closes the privilege-escalation gap
    /// where an Admin (2) could previously mint a new MasterAdmin (1) account.
    /// Only a MasterAdmin may create another MasterAdmin.
    /// </summary>
    [HttpPost("register-employee")]
    [Authorize(Policy = "MasterAdminOrAdmin")]
    public async Task<IActionResult> RegisterEmployee(RegisterEmployeeRequest request)
    {
        request.Email = request.Email.Trim();
        if (!IsValidEmail(request.Email))
            return BadRequest("A valid email address is required.");

        var passwordError = ValidatePassword(request.Password);
        if (passwordError != null)
            return BadRequest(passwordError);

        var callerUserTypeId = long.Parse(User.FindFirst("UserTypeId")!.Value);

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
            var refresh = await IssueRefreshTokenAsync(null, employee.EmployeeId);
            return Ok(new AuthResponse
            {
                Token = employeeToken,
                Email = employee.Email,
                FullName = $"{employee.FirstName} {employee.LastName}",
                AccountType = "Employee",
                UserTypeId = employee.UserTypeId,
                RefreshToken = refresh.Token,
                RefreshTokenExpiresAt = refresh.ExpiresAt
            });
        }

        return Unauthorized("Invalid email or password.");
    }

    /// <summary>
    /// Exchanges a still-valid refresh token for a fresh JWT. The presented
    /// token is rotated: the old row is revoked and a brand-new refresh token
    /// is issued, so a leaked/stolen token can only be used once.
    /// </summary>
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshRequest request)
    {
        var hash = JwtService.HashRefreshToken(request.RefreshToken);
        var stored = await _context.RefreshTokens.FirstOrDefaultAsync(r => r.TokenHash == hash);
        if (stored == null)
            return Unauthorized("Invalid refresh token.");

        if (stored.RevokedAt != null)
            return Unauthorized("Refresh token has been revoked.");

        if (stored.ExpiresAt <= DateTime.UtcNow)
            return Unauthorized("Refresh token has expired.");

        // Re-check the owning account is still active before issuing a new JWT.
        if (stored.CustomerId.HasValue)
        {
            var customer = await _context.CustomerMasters.FindAsync(stored.CustomerId.Value);
            if (customer == null || !customer.IsActive)
                return Unauthorized("Account is not active.");
        }
        else if (stored.EmployeeId.HasValue)
        {
            var employee = await _context.EmployeeMasters.FindAsync(stored.EmployeeId.Value);
            if (employee == null || !employee.IsActive)
                return Unauthorized("Account is not active.");
        }

        stored.RevokedAt = DateTime.UtcNow;
        var refresh = await IssueRefreshTokenAsync(stored.CustomerId, stored.EmployeeId);
        await _context.SaveChangesAsync();

        var accountType = stored.CustomerId.HasValue ? "Customer" : "Employee";
        var (id, email, fullName, userTypeId) = accountType switch
        {
            "Customer" when stored.CustomerId.HasValue =>
                await GetCustomerIdentityAsync(stored.CustomerId.Value),
            _ => await GetEmployeeIdentityAsync(stored.EmployeeId!.Value)
        };

        if (id == null)
            return Unauthorized("Unable to reissue token for this account.");

        var newToken = _jwtService.GenerateToken(id.Value, email, accountType, userTypeId);
        return Ok(new AuthResponse
        {
            Token = newToken,
            Email = email,
            FullName = fullName,
            AccountType = accountType,
            UserTypeId = userTypeId,
            RefreshToken = refresh.Token,
            RefreshTokenExpiresAt = refresh.ExpiresAt
        });
    }

    /// <summary>
    /// Revokes the presented refresh token (logout). The raw token still hashes
    /// to the stored row, but the row is marked revoked so any subsequent
    /// POST /api/auth/refresh with it is rejected.
    /// </summary>
    [HttpPost("revoke")]
    [Authorize]
    public async Task<IActionResult> Revoke(RefreshRequest request)
    {
        var hash = JwtService.HashRefreshToken(request.RefreshToken);
        var stored = await _context.RefreshTokens.FirstOrDefaultAsync(r => r.TokenHash == hash);
        if (stored == null)
            return BadRequest("Invalid refresh token.");

        stored.RevokedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    /// <summary>Persists a new refresh token row; returns the raw token once.</summary>
    private async Task<(string Token, DateTime ExpiresAt)> IssueRefreshTokenAsync(long? customerId, long? employeeId)
    {
        var (token, hash) = JwtService.GenerateRefreshToken();
        var expiresAt = DateTime.UtcNow.Add(RefreshLifetime);
        _context.RefreshTokens.Add(new RefreshToken
        {
            TokenHash = hash,
            CustomerId = customerId,
            EmployeeId = employeeId,
            IssuedAt = DateTime.UtcNow,
            ExpiresAt = expiresAt
        });
        await _context.SaveChangesAsync();
        return (token, expiresAt);
    }

    private async Task<(long? Id, string Email, string FullName, long? UserTypeId)> GetCustomerIdentityAsync(long customerId)
    {
        var customer = await _context.CustomerMasters.FindAsync(customerId);
        return customer == null
            ? (null, "", "", null)
            : (customer.CustomerId, customer.Email, $"{customer.FirstName} {customer.LastName}", null);
    }

    private async Task<(long? Id, string Email, string FullName, long? UserTypeId)> GetEmployeeIdentityAsync(long employeeId)
    {
        var employee = await _context.EmployeeMasters.FindAsync(employeeId);
        return employee == null
            ? (null, "", "", null)
            : (employee.EmployeeId, employee.Email, $"{employee.FirstName} {employee.LastName}", employee.UserTypeId);
    }

    private static bool IsValidEmail(string email) =>
        email.Length <= 254 && new EmailAddressAttribute().IsValid(email);

    /// <summary>Returns an error message string if the password is too weak, else null.</summary>
    private static string? ValidatePassword(string password)
    {
        if (password.Length < 8) return "Password must be at least 8 characters long.";
        if (!password.Any(char.IsUpper)) return "Password must contain at least one uppercase letter.";
        if (!password.Any(char.IsLower)) return "Password must contain at least one lowercase letter.";
        if (!password.Any(char.IsDigit)) return "Password must contain at least one number.";
        return null;
    }
}
