using InterviewTestInvoiceAPI.Authentications;
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
            var secretKey = _configuration["Authentication:SecretKey"]!.ToString();
            var issuer = _configuration["Authentication:Issuer"]!.ToString();
            var audience = _configuration["Authentication:Audience"]!.ToString();

            return AuthToken.GenerateToken(secretKey, issuer, audience);
        }

        private bool IsTokenValid(string token)
        {
            var secretKey = _configuration["Authentication:SecretKey"]!.ToString();
            var issuer = _configuration["Authentication:Issuer"]!.ToString();
            var audience = _configuration["Authentication:Audience"]!.ToString();

            return AuthToken.IsTokenValid(token, secretKey, issuer, audience);
        }
    }
}
