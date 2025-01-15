using InterviewTestInvoiceAPI.Authentications;

namespace InterviewInvoice_xUnit
{
    public class UnitTest1
    {
        [Fact]
        public void Generate_And_Validate_Token()
        {
            var expectedValue = true;
            var secretKey = "this is my custom Secret key for authentication.";
            var issuer = "singh";
            var audience = "https://localhost:7144/";

            var token = AuthToken.GenerateToken(secretKey, issuer, audience);
            var result = AuthToken.IsTokenValid(token, secretKey, issuer, audience);

            Assert.Equal(expectedValue, result);
        }
    }
}