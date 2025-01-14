using InterviewTestInvoiceAPI.Models;
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
    [Authorize]
    public class TestInvoiceController : ControllerBase
    {
        private readonly ILogger<TestInvoiceController> _logger;
        private readonly ITestInvoice _testInvoice;
        private readonly IConfiguration _configuration;
        public TestInvoiceController(ITestInvoice testInvoice, ILogger<TestInvoiceController> logger, IConfiguration configuration) 
        {
            _logger = logger;
            _testInvoice = testInvoice;
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
        public async Task<IActionResult> GetInvoices()
        {
            var testInvoice = await _testInvoice.GetInvoicesAsync();
            _logger.LogInformation("GetInvoices: " + testInvoice?.Count());
            return Ok(testInvoice);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoiceById(Guid id)
        {
            var testInvoice = await _testInvoice.GetInvoiceByIdAsync(id);
            if (testInvoice == null)
                return NotFound();

            _logger.LogInformation("GetInvoiceById: " + testInvoice.Id);
            return Ok(testInvoice);
        }

        [HttpPost]
        public async Task<IActionResult> CreateInvoice([FromBody] TestInvoice testInvoice)
        {
            testInvoice.Id = Guid.NewGuid();
            testInvoice.CreateOn = DateTime.UtcNow;
            testInvoice.UpdateOn = DateTime.UtcNow;
            await _testInvoice.CreateInvoiceAsync(testInvoice);

            _logger.LogInformation("CreateInvoice: " + testInvoice.Id);
            return Ok(testInvoice);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvoice(Guid id, [FromBody] TestInvoice testInvoice)
        {
            var invoice = await _testInvoice.GetInvoiceByIdAsync(id);
            if (invoice == null)
                return NotFound();

            testInvoice.UpdateOn = DateTime.UtcNow;
            await _testInvoice.UpdateInvoiceAsync(testInvoice);

            _logger.LogInformation("UpdateInvoice: " + invoice.Id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(Guid id)
        {
            var testInvoice = await _testInvoice.GetInvoiceByIdAsync(id);
            if (testInvoice == null)
                return NotFound();

            _logger.LogInformation("DeleteInvoice: " + testInvoice.Id);
            return (IActionResult)testInvoice;
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
    }
}
