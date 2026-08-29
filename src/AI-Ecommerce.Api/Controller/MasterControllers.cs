using AI_Ecommerce.Api.Controllers.Abstractions;
using AI_Ecommerce.Api.Services;
using AI_Ecommerce.Data;
using AI_Ecommerce.Data.Models.Masters;
using AI_Ecommerce.Data.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AI_Ecommerce.Api.Controllers
{
    // ============================================================
    // Simple master controllers — all share the generic CRUD base.
    // Reads are allowed for any authenticated user; writes require an
    // Employee JWT (CreatedBy/ModifiedBy/DeletedBy = caller's EmployeeId).
    // ============================================================

    [ApiController]
    [Route("api/product-master")]
    [Authorize]
    public class ProductMastersController : MasterCrudControllerBase<ProductMaster>
    {
        public ProductMastersController(ApplicationDbContext context) : base(context) { }
    }

    [ApiController]
    [Route("api/category-master")]
    [Authorize]
    public class CategoryMastersController : MasterCrudControllerBase<CategoryMaster>
    {
        public CategoryMastersController(ApplicationDbContext context) : base(context) { }
    }

    [ApiController]
    [Route("api/subcategory-master")]
    [Authorize]
    public class SubCategoryMastersController : MasterCrudControllerBase<SubCategoryMaster>
    {
        public SubCategoryMastersController(ApplicationDbContext context) : base(context) { }
    }

    [ApiController]
    [Route("api/unit-master")]
    [Authorize]
    public class UnitMastersController : MasterCrudControllerBase<UnitMaster>
    {
        public UnitMastersController(ApplicationDbContext context) : base(context) { }
    }

    [ApiController]
    [Route("api/warehouse-master")]
    [Authorize]
    public class WarehouseMastersController : MasterCrudControllerBase<WarehouseMaster>
    {
        public WarehouseMastersController(ApplicationDbContext context) : base(context) { }
    }

    [ApiController]
    [Route("api/vendor-master")]
    [Authorize]
    public class VendorMastersController : MasterCrudControllerBase<VendorMaster>
    {
        public VendorMastersController(ApplicationDbContext context) : base(context) { }
    }

    [ApiController]
    [Route("api/rawmaterial-master")]
    [Authorize]
    public class RawMaterialMastersController : MasterCrudControllerBase<RawMaterialMaster>
    {
        public RawMaterialMastersController(ApplicationDbContext context) : base(context) { }
    }

    [ApiController]
    [Route("api/department-master")]
    [Authorize]
    public class DepartmentMastersController : MasterCrudControllerBase<DepartmentMaster>
    {
        public DepartmentMastersController(ApplicationDbContext context) : base(context) { }
    }

    [ApiController]
    [Route("api/usertype-master")]
    [Authorize]
    public class UserTypeMastersController : MasterCrudControllerBase<UserTypeMaster>
    {
        public UserTypeMastersController(ApplicationDbContext context) : base(context) { }
    }

    // ============================================================
    // Customer Master — employee-managed. Never returns PasswordHash.
    // ============================================================

    [ApiController]
    [Route("api/customer-master")]
    [Authorize]
    public class CustomerMastersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CustomerMastersController(ApplicationDbContext context) => _context = context;

        private long? CurrentEmployeeId =>
            User.FindFirst("AccountType")?.Value == "Employee" &&
            long.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var id) ? id : null;

        private bool IsEmployee() => CurrentEmployeeId.HasValue;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var rows = await _context.CustomerMasters
                .OrderByDescending(c => c.CreatedAt)
                .Select(c => new CustomerDto
                {
                    CustomerId = c.CustomerId,
                    Email = c.Email,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    PhoneNumber = c.PhoneNumber,
                    AddressLine = c.AddressLine,
                    City = c.City,
                    State = c.State,
                    Country = c.Country,
                    Pincode = c.Pincode,
                    IsActive = c.IsActive,
                    CreatedAt = c.CreatedAt
                })
                .ToListAsync();
            return Ok(rows);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            var row = await _context.CustomerMasters
                .Where(c => c.CustomerId == id)
                .Select(c => new CustomerDto
                {
                    CustomerId = c.CustomerId,
                    Email = c.Email,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    PhoneNumber = c.PhoneNumber,
                    AddressLine = c.AddressLine,
                    City = c.City,
                    State = c.State,
                    Country = c.Country,
                    Pincode = c.Pincode,
                    IsActive = c.IsActive,
                    CreatedAt = c.CreatedAt
                })
                .FirstOrDefaultAsync();
            return row == null ? NotFound() : Ok(row);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerRequest request)
        {
            if (!IsEmployee()) return Forbid();
            if (await _context.CustomerMasters.AnyAsync(c => c.Email == request.Email))
                return BadRequest("Email already exists.");

            var customer = new CustomerMaster
            {
                Email = request.Email,
                PasswordHash = PasswordHasher.HashPassword(request.Password),
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                AddressLine = request.AddressLine,
                City = request.City,
                State = request.State,
                Country = request.Country,
                Pincode = request.Pincode,
                IsActive = true
            };
            MasterAudit.StampCreate(customer, CurrentEmployeeId);
            _context.CustomerMasters.Add(customer);
            await _context.SaveChangesAsync();
            return Ok(new { customer.CustomerId, customer.Email });
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, UpdateCustomerRequest request)
        {
            if (!IsEmployee()) return Forbid();
            var customer = await _context.CustomerMasters.FindAsync(id);
            if (customer == null) return NotFound();

            customer.FirstName = request.FirstName;
            customer.LastName = request.LastName;
            customer.PhoneNumber = request.PhoneNumber;
            customer.AddressLine = request.AddressLine;
            customer.City = request.City;
            customer.State = request.State;
            customer.Country = request.Country;
            customer.Pincode = request.Pincode;
            customer.IsActive = request.IsActive;
            MasterAudit.StampUpdate(customer, CurrentEmployeeId);
            await _context.SaveChangesAsync();
            return Ok(new { customer.CustomerId, customer.Email });
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            if (!IsEmployee()) return Forbid();
            var customer = await _context.CustomerMasters.FindAsync(id);
            if (customer == null) return NotFound();
            MasterAudit.StampDelete(customer, CurrentEmployeeId);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

    // ============================================================
    // Employee Master — staff management. Never returns PasswordHash.
    // Enforces the same privilege rule as /api/auth/register-employee:
    // a caller can only create accounts at or below their own UserTypeId.
    // ============================================================

    [ApiController]
    [Route("api/employee-master")]
    [Authorize]
    public class EmployeeMastersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeeMastersController(ApplicationDbContext context) => _context = context;

        private long? CurrentEmployeeId =>
            User.FindFirst("AccountType")?.Value == "Employee" &&
            long.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var id) ? id : null;

        private long? CallerUserTypeId =>
            long.TryParse(User.FindFirst("UserTypeId")?.Value, out var id) ? id : null;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var rows = await _context.EmployeeMasters
                .OrderByDescending(e => e.CreatedAt)
                .Select(e => new EmployeeDto
                {
                    EmployeeId = e.EmployeeId,
                    Email = e.Email,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    PhoneNumber = e.PhoneNumber,
                    DepartmentId = e.DepartmentId,
                    UserTypeId = e.UserTypeId,
                    DepartmentName = e.Department!.DepartmentName,
                    UserTypeName = e.UserType!.UserTypeName,
                    IsActive = e.IsActive,
                    CreatedAt = e.CreatedAt
                })
                .ToListAsync();
            return Ok(rows);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            var row = await _context.EmployeeMasters
                .Where(e => e.EmployeeId == id)
                .Select(e => new EmployeeDto
                {
                    EmployeeId = e.EmployeeId,
                    Email = e.Email,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    PhoneNumber = e.PhoneNumber,
                    DepartmentId = e.DepartmentId,
                    UserTypeId = e.UserTypeId,
                    DepartmentName = e.Department!.DepartmentName,
                    UserTypeName = e.UserType!.UserTypeName,
                    IsActive = e.IsActive,
                    CreatedAt = e.CreatedAt
                })
                .FirstOrDefaultAsync();
            return row == null ? NotFound() : Ok(row);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeRequest request)
        {
            var callerUserTypeId = CallerUserTypeId;
            if (callerUserTypeId is not (1 or 2)) return Forbid();

            if (!await _context.UserTypeMasters.AnyAsync(t => t.UserTypeId == request.UserTypeId))
                return BadRequest("Invalid UserTypeId.");
            if (request.UserTypeId < callerUserTypeId)
                return Forbid();
            if (!await _context.DepartmentMasters.AnyAsync(d => d.DepartmentId == request.DepartmentId))
                return BadRequest("Invalid DepartmentId.");
            if (await _context.EmployeeMasters.AnyAsync(e => e.Email == request.Email))
                return BadRequest("Email already exists.");

            var employee = new EmployeeMaster
            {
                Email = request.Email,
                PasswordHash = PasswordHasher.HashPassword(request.Password),
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                DepartmentId = request.DepartmentId,
                UserTypeId = request.UserTypeId,
                IsActive = true
            };
            MasterAudit.StampCreate(employee, CurrentEmployeeId);
            _context.EmployeeMasters.Add(employee);
            await _context.SaveChangesAsync();
            return Ok(new { employee.EmployeeId, employee.Email, employee.UserTypeId });
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, UpdateEmployeeRequest request)
        {
            var callerUserTypeId = CallerUserTypeId;
            if (callerUserTypeId is not (1 or 2)) return Forbid();

            var employee = await _context.EmployeeMasters.FindAsync(id);
            if (employee == null) return NotFound();

            // Only a MasterAdmin can change another MasterAdmin; and no caller
            // can grant a role more privileged than their own.
            if (request.UserTypeId < callerUserTypeId) return Forbid();
            if (employee.UserTypeId == 1 && callerUserTypeId != 1) return Forbid();

            employee.FirstName = request.FirstName;
            employee.LastName = request.LastName;
            employee.PhoneNumber = request.PhoneNumber;
            employee.DepartmentId = request.DepartmentId;
            employee.UserTypeId = request.UserTypeId;
            employee.IsActive = request.IsActive;
            MasterAudit.StampUpdate(employee, CurrentEmployeeId);
            await _context.SaveChangesAsync();
            return Ok(new { employee.EmployeeId, employee.Email });
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            var callerUserTypeId = CallerUserTypeId;
            if (callerUserTypeId is not (1 or 2)) return Forbid();

            var employee = await _context.EmployeeMasters.FindAsync(id);
            if (employee == null) return NotFound();
            if (employee.UserTypeId == 1 && callerUserTypeId != 1) return Forbid();

            MasterAudit.StampDelete(employee, CurrentEmployeeId);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

    // ============================================================
    // DTOs for the two secure master endpoints
    // ============================================================

    public class CustomerDto
    {
        public long CustomerId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? AddressLine { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? Pincode { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateCustomerRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? AddressLine { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? Pincode { get; set; }
    }

    public class UpdateCustomerRequest
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? AddressLine { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? Pincode { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public class EmployeeDto
    {
        public long EmployeeId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public long DepartmentId { get; set; }
        public long UserTypeId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public string UserTypeName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateEmployeeRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public long DepartmentId { get; set; }
        public long UserTypeId { get; set; } = 5;
    }

    public class UpdateEmployeeRequest
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public long DepartmentId { get; set; }
        public long UserTypeId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}