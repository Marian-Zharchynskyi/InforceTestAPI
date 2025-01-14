using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.ViewModels;
using Domain.Authentications;
using Domain.Authentications.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services.TokenService
{
    public class JwtTokenService(IConfiguration configuration) : IJwtTokenService
    {
        private JwtSecurityToken GenerateAccessToken(User user)
        {
            var issuer = configuration["AuthSettings:issuer"];
            var audience = configuration["AuthSettings:audience"];
            var keyString = configuration["AuthSettings:key"];
            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString!));

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("id", user.Id.Value.ToString()),
                new Claim("email", user.Email!),
                new Claim("name", user.Name ?? "N/A"),
            };

            if (user.Roles.Count() > 0)
            {
                var roleClaims = user.Roles.Select(ur => new Claim(
                    "role",
                    ur.Name
                )).ToArray();

                claims.AddRange(roleClaims);
            }
            else
            {
                claims.Add(new Claim("role", AuthSettings.UserRole));
            }
            
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }
        
        private string GenerateRefreshToken()
        {
            var bytes = new byte[32];

            using (var rnd = RandomNumberGenerator.Create())
            {
                rnd.GetBytes(bytes);
                return Convert.ToBase64String(bytes);
            }
        }
        
        public ClaimsPrincipal GetPrincipals(string accessToken)
        {
            var jwtSecurityKey = configuration["AuthSettings:key"];

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecurityKey))
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var principals = tokenHandler.ValidateToken(accessToken, validationParameters, out SecurityToken securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if(jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
            {
                throw new SecurityTokenException("Invalid access token");
            }

            return principals;
        }
        
        public async Task<JwtVM> GenerateTokensAsync(User user, CancellationToken cancellationToken)
        {
            var accessToken = GenerateAccessToken(user);
            
            var tokens = new JwtVM
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
            };

            return tokens;
        }
    }
}
