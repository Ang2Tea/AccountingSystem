using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AccountingSystem.Application.Contract.Users;
using AccountingSystem.Domain.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AccountingSystem.Application.Users
{
    public class AuthAppService : IAuthAppService
    {
        private readonly IConfiguration _configuration;

        public AuthAppService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Task<string> Auth(UserDto user)
        {
            var handler = new JwtSecurityTokenHandler();
  
            var privateKey = Encoding.UTF8.GetBytes(_configuration["PrivateKey"]);
          
            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(privateKey),
                SecurityAlgorithms.HmacSha256);
  
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddHours(1),
                Subject = GenerateClaims(user)
            };
          
            var token = handler.CreateToken(tokenDescriptor);
            return Task.FromResult(handler.WriteToken(token));
        }
  
        private static ClaimsIdentity GenerateClaims(UserDto user)
        {
            var ci = new ClaimsIdentity();
  
            ci.AddClaim(new Claim("id", user.Id.ToString()));
            ci.AddClaim(new Claim(ClaimTypes.Name, user.Login));
            ci.AddClaim(new Claim(ClaimTypes.GivenName, user.Name));
            ci.AddClaim(new Claim(ClaimTypes.Email, user.Email));

            foreach (var role in user.Roles)
            {
                ci.AddClaim(new Claim(ClaimTypes.Role, role));
            }
          
            return ci;
        }
    }
}