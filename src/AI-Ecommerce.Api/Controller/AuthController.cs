using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using AI_Ecommerce.Data;
using AI_Ecommerce.Data.Models;
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
        public int UserType { get; set; } = 4; // Default: Customer
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
        public int UserType { get; set; }
    }

    public class RegisterEmployeeRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }

        /// <summary>3: Employee, 2: Master, 1: Master Admin. Defaults to Employee.</summary>
        public int UserType { get; set; } = 3;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        if (await _context.Users.AnyAsync(u => u.Email == request.Email))
            return BadRequest("Email already exists.");

        // Public self-registration is always a Customer account (4). UserType is
        // intentionally NOT taken from the request body — otherwise anyone could
        // self-register as Master Admin. Staff accounts are created separately via
        // POST /api/auth/register-employee, which requires an authenticated
        // Master Admin/Master caller.
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            PasswordHash = PasswordHasher.HashPassword(request.Password),
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            UserType = 4,
            IsActive = true
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var token = _jwtService.GenerateToken(user.Id, user.Email, user.UserType);

        return Ok(new AuthResponse
        {
            Token = token,
            Email = user.Email,
            FullName = $"{user.FirstName} {user.LastName}",
            UserType = user.UserType
        });
    }

    /// <summary>
    /// Creates a staff account (Employee/Master/Master Admin). Restricted to
    /// callers who are already Master Admin (1) or Master (2) — see the
    /// [Authorize] + role check below. This does NOT log the caller in; it
    /// returns the new account's details so an admin can hand off credentials.
    /// </summary>
    [HttpPost("register-employee")]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public async Task<IActionResult> RegisterEmployee(RegisterEmployeeRequest request)
    {
        var callerUserType = User.FindFirst("UserType")?.Value;
        if (callerUserType != "1" && callerUserType != "2")
            return Forbid();

        if (request.UserType < 1 || request.UserType > 4)
            return BadRequest("Invalid UserType.");

        if (await _context.Users.AnyAsync(u => u.Email == request.Email))
            return BadRequest("Email already exists.");

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            PasswordHash = PasswordHasher.HashPassword(request.Password),
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            UserType = request.UserType,
            IsActive = true
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            user.Email,
            FullName = $"{user.FirstName} {user.LastName}",
            user.UserType
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (user == null || !PasswordHasher.VerifyPassword(request.Password, user.PasswordHash))
            return Unauthorized("Invalid email or password.");

        user.LastLoginAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        var token = _jwtService.GenerateToken(user.Id, user.Email, user.UserType);

        return Ok(new AuthResponse
        {
            Token = token,
            Email = user.Email,
            FullName = $"{user.FirstName} {user.LastName}",
            UserType = user.UserType
        });
    }
}