using HotelBackend.Identity.Common.Interfaces;
using HotelBackend.Identity.Common.Models;
using HotelBackend.Identity.Common.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace HotelBackend.Identity.Services
{
    public class JwtTokenService
        : IJwtTokenService
    {
        public DateTime RefreshTokenValidityInDays => DateTime.UtcNow
            .AddDays(_jwtSettings.RefreshTokenValidityInDays);

        private readonly JwtSettings _jwtSettings;
        private readonly TokenValidationParameters _expiredAccessTokenValidationParameters;

        public JwtTokenService(
            IOptionsMonitor<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.CurrentValue
                ?? throw new ArgumentNullException(nameof(jwtSettings));

            _expiredAccessTokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtSettings.Issuer
                    ?? throw new ArgumentNullException(_jwtSettings.Issuer),
                ValidAudience = _jwtSettings.Audience
                    ?? throw new ArgumentNullException(_jwtSettings.Audience),
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtSettings.Secret
                        ?? throw new ArgumentNullException(_jwtSettings.Secret))),
            };
        }


        public string CreateAccessToken(
            ApplicationUser user,
            IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.UserName!),
                new(ClaimTypes.Email, user.Email!)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.Expire),
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string CreateAccessToken(ClaimsPrincipal principal)
        {
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: principal.Claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.Expire),
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string CreateRefreshToken()
        {
            Span<byte> randomNumber = stackalloc byte[64];
            RandomNumberGenerator.Fill(randomNumber);
            return Convert.ToBase64String(randomNumber)
                .Replace('/', '_')
                .Replace('+', '-')
                .Replace("=", "");
        }

        public ClaimsPrincipal? ValidateExpiredAccessToken(
            string expiredAccessToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                var principal = tokenHandler
                    .ValidateToken(
                        expiredAccessToken,
                        _expiredAccessTokenValidationParameters,
                        out var securityToken);

                if (securityToken is not JwtSecurityToken jwtToken ||
                    !jwtToken.Header.Alg
                        .Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    return null;
                }

                return principal;
            }
            catch
            {
                return null; 
            }
        }
    }
}
