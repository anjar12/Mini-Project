using Microsoft.IdentityModel.Tokens;
using Shared.Data.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Gateway.Services
{
    public class TokenService
    {
        public IConfiguration Configuration { get; }
        public TokenService(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public AuthToken GenerateToken(string stringkey, string modul)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration[stringkey + ":Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var expirationDate = DateTime.Now.AddHours(1);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, modul.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(audience: Configuration[stringkey + ":Audience"],
                                             issuer: Configuration[stringkey + ":Issuer"],
                                             claims: claims,
                                             expires: expirationDate,
                                             signingCredentials: credentials);
            var authToken = new AuthToken();
            authToken.Token = new JwtSecurityTokenHandler().WriteToken(token);
            authToken.ExpirationDate = expirationDate;
            return authToken;
        }
        public AuthToken PageGenerateToken(string stringkey, string modul)
        {
            var authToken = new AuthToken();
            authToken = GenerateToken(stringkey, modul);
            return authToken;
        }
    }
}
