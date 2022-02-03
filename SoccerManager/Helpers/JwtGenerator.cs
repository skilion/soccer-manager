using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SoccerManager.Helpers
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly string issuerSigningKeyConfig = "JwtSettings:IssuerSigningKey";
        private readonly string expirationTimeConfig = "JwtSettings:ExpirationTimeMinutes";

        private readonly SigningCredentials signinCredentials;
        private readonly int expirationTimeMinutes;

        public JwtGenerator(IConfiguration configuration)
        {
            var issuerSigningKey = configuration[issuerSigningKeyConfig];
            if (issuerSigningKey is null)
            {
                throw new ArgumentNullException(issuerSigningKeyConfig);
            }
            var expirationTimeMinutesStr = configuration[expirationTimeConfig];
            if (expirationTimeMinutesStr is null)
            {
                throw new ArgumentNullException(expirationTimeConfig);
            }
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(issuerSigningKey));
            signinCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            expirationTimeMinutes = int.Parse(expirationTimeMinutesStr);
        }

        public string Generate(string email)
        {
            var token = new JwtSecurityToken(
                claims: new List<Claim>()
                {
                    new Claim(ClaimTypes.Email, email),
                },
                expires: DateTime.Now.AddMinutes(expirationTimeMinutes),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }
    }
}
