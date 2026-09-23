using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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
            var secret = _config["Jwt:Secret"]
                ?? throw new InvalidOperationException("Jwt:Secret is not configured.");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
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

        /// <summary>
        /// Generates a new opaque refresh token (64 random bytes, base64) and
        /// returns its SHA-256 hash for storage. Only the hash is ever persisted;
        /// the raw token itself is returned solely to the caller for one-time
        /// delivery to the client.
        /// </summary>
        public static (string Token, string TokenHash) GenerateRefreshToken()
        {
            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            var hash = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(token)));
            return (token, hash);
        }

        /// <summary>Computes the stored SHA-256 hash of a raw refresh token.</summary>
        public static string HashRefreshToken(string token) =>
            Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(token)));
    }
}