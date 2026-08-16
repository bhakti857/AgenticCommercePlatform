using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace AI_Ecommerce.Api.Services
{
    public class JwtService
    {
        private readonly IConfiguration _config;

        public JwtService(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Generates a JWT for either account type. <paramref name="accountId"/> is the
        /// long primary key (CustomerMaster.CustomerId or EmployeeMaster.EmployeeId) and
        /// is placed in the `sub`/NameIdentifier claim so controllers can parse it as a
        /// long. <paramref name="accountType"/> is "Customer" or "Employee" — used by the
        /// agent endpoint to reject customers outright. <paramref name="userTypeId"/> is
        /// only set for employees (maps to UserTypeMaster: 1 MasterAdmin, 2 Admin, 3
        /// Senior, 4 Junior, 5 User) and is null for customers.
        /// </summary>
        public string GenerateToken(long accountId, string email, string accountType, long? userTypeId)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claimsList = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, accountId.ToString()),
                new Claim(ClaimTypes.NameIdentifier, accountId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim("AccountType", accountType),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            if (userTypeId.HasValue)
                claimsList.Add(new Claim("UserTypeId", userTypeId.Value.ToString()));

            var claims = claimsList.ToArray();

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}