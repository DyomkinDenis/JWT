using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWT
{
    public static class TokenService
    {

        public static string GenerateToken()
        {
            return GenerateToken("jwtApi");
        }
        public static string GenerateToken(string scope)
        {
            var claims = new Claim[]
                {
                   new Claim(ClaimTypes.Name, "Denis")
                };


            var authClaims = new List<Claim>
            {
               new Claim(ClaimTypes.Name, "Denis"),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),//уникальный айди токена, можно сделать его одноразовым на апи)
               new Claim(JwtRegisteredClaimNames.Aud, "ClientId"),
            };


            authClaims.Add(new Claim(ClaimTypes.Role, "Administrator"));


            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("W1Kh123h4j3jh1kakfkMRD87S4S5SAK4KLSD53");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "https://localhost:7114",
                TokenType = "at+jwt",
                Subject = new ClaimsIdentity(authClaims),
                Audience = scope,
                //Subject = new ClaimsIdentity(claims, "JWT"),
                Expires = DateTime.UtcNow.AddSeconds(3600),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);

            return jwt;
        }
    }
}
