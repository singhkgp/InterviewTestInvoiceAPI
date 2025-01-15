using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InterviewTestInvoiceAPI.Authentications
{
    public class AuthToken
    {
        public AuthToken() { }

        public static string GenerateToken(string secretKey, string issuer, string audience)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "singh"),
                new Claim(ClaimTypes.Role, "Admin")
            };
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static bool IsTokenValid(string token, string secretKey, string issuer, string audience)
        {
            if (string.IsNullOrEmpty(token)) return false;
            var key = Encoding.UTF8.GetBytes(secretKey!.ToString());

            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = issuer,
                ValidateAudience = true,
                ValidAudience = audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                tokenHandler.ValidateToken(token, validationParameters, out SecurityToken _);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
