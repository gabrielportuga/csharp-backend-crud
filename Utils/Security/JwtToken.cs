using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KanbanBoard.Api.Utils.Configurations;
using Microsoft.IdentityModel.Tokens;

namespace KanbanBoard.Api.Utils.Security
{
    public class JwtToken : IJwtToken
    {
        public string GenerateJwtToken(string username)
        {
            string secretKey = KanbanBoardConfig.JwtSecretKey;
            string issuer = KanbanBoardConfig.JwtIssuer;
            string audience = KanbanBoardConfig.JwtAudience;
            double expireMinutes = KanbanBoardConfig.ExpireMinutes;

            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(expireMinutes),
                Audience = audience,
                Issuer = issuer,
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);
            string jwtToken = tokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}
