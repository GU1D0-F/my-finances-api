using MyFinances.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyFinances.Domain.Users.Services
{
    public class UserTokenService : IUserTokenService
    {
        private const string issuer = "glk-finances-identity";
        private const string audience = "glk-finances-apps";
        private readonly IConfiguration _configuration; //TODO: Buscar infos acima por variaveis de ambiente
        public UserTokenService()
        {

        }


        public string GenerateToken(User user)
        {
            Claim[] claims = new Claim[]
            {
                new Claim("sub", user.UserName),
                new Claim("id", user.Id),
                new Claim("loginTimestamp", DateTime.UtcNow.ToString())
            };

            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("3A81F90D5A4E5E8A1C84E7D4B901D6BB67F4FD52415A84ECA7857D2B2E1A55F"));

            var signingCredentials =
                new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
                (
                    issuer: issuer,
                    audience: audience,
                    expires: DateTime.Now.AddHours(5),
                    claims: claims,
                    signingCredentials: signingCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
