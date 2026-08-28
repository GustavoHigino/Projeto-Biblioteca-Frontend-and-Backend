using Microsoft.IdentityModel.Tokens;
using projetobiblioteca.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace projetobiblioteca.Tools.Bearer.Implementation
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly IConfiguration _configuration;
        public TokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateAccessToken
            (IEnumerable<Claim> claims)
        {
            var secretKey =
                new SymmetricSecurityKey(Encoding
                .UTF8.GetBytes(_configuration[
                    "TokenConfiguration:Secret"]));
            var signingCredentials = new
                SigningCredentials
                (secretKey,
                SecurityAlgorithms.HmacSha256);
            var tokenOptions = new
                JwtSecurityToken(
                issuer: _configuration
                ["TokenConfiguration:Issuer"],
                audience: _configuration
                ["TokenConfiguration:Audience"],
                claims: claims,
                expires: DateTime.UtcNow
                .AddMinutes(Convert.ToInt32
                (_configuration
                ["TokenConfiguration:Minutes"])),
                signingCredentials: signingCredentials);
            return new JwtSecurityTokenHandler()
                .WriteToken(tokenOptions);
                
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = 
                RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert
                .ToBase64String(randomNumber);
        }

        public ClaimsPrincipal 
            GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new
                TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(
                    _configuration
                ["TokenConfiguration:Secret"])),
                ValidateLifetime=false
            };
            var tokenHandler = new
                JwtSecurityTokenHandler();
            var principal = tokenHandler
                .ValidateToken
                (token, tokenValidationParameters,
                out SecurityToken securityToken);
            return principal;
        }
    }
}
