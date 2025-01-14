using InterviewTestInvoiceAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InterviewTestInvoiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<TestInvoiceController> _logger;
        private readonly IConfiguration _configuration;
        public LoginController(ILogger<TestInvoiceController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }


        [HttpGet]
        [Route("token")]
        public IActionResult GetToken()
        {
            string token = GenerateToken();
            _logger.LogInformation("Token created: ");
            return Ok(new { Token = token });
        }

        [HttpGet]
        [Route("validate-token/{token}")]
        public IActionResult GetValidateToken(string token)
        {
            var status = IsTokenValid(token);
            _logger.LogInformation("IsTokenValid: " + status);
            return Ok(new { Token = status ? "Success" : "Failed" });
        }

        private string GenerateToken()
        {
            var key = _configuration["Authentication:SecretKey"];
            var issuer = _configuration["Authentication:Issuer"];
            var audience = _configuration["Authentication:Audience"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!.ToString()));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
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

        private bool IsTokenValid(string token)
        {
            var secretKey = _configuration["Authentication:SecretKey"];
            var issuer = _configuration["Authentication:Issuer"];
            var audience = _configuration["Authentication:Audience"];

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
