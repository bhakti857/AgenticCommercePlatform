using AI_Ecommerce.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AI_Ecommerce.Api.Controllers
{
    /// <summary>Customer profile — the address fields already exist on CustomerMaster.</summary>
    [ApiController]
    [Route("api/profile")]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProfileController(ApplicationDbContext context) => _context = context;

        private long GetCustomerId()
        {
            if (User.FindFirst("AccountType")?.Value != "Customer")
                throw new UnauthorizedAccessException("Only customer accounts have a profile.");
            var sub = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return long.Parse(sub!);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var customerId = GetCustomerId();
            var profile = await _context.CustomerMasters
                .Where(c => c.CustomerId == customerId)
                .Select(c => new ProfileDto
                {
                    Email = c.Email,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    PhoneNumber = c.PhoneNumber,
                    AddressLine = c.AddressLine,
                    City = c.City,
                    State = c.State,
                    Country = c.Country,
                    Pincode = c.Pincode
                })
                .FirstOrDefaultAsync();
            return profile == null ? NotFound() : Ok(profile);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProfileDto request)
        {
            var customerId = GetCustomerId();
            var customer = await _context.CustomerMasters.FindAsync(customerId);
            if (customer == null) return NotFound();

            customer.FirstName = request.FirstName;
            customer.LastName = request.LastName;
            customer.PhoneNumber = request.PhoneNumber;
            customer.AddressLine = request.AddressLine;
            customer.City = request.City;
            customer.State = request.State;
            customer.Country = request.Country;
            customer.Pincode = request.Pincode;
            customer.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Profile updated." });
        }
    }

    public class ProfileDto
    {
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? AddressLine { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? Pincode { get; set; }
    }
}