using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AccountingSystem.Application.Contract.Users;
using AccountingSystem.Domain.Core;
using AccountingSystem.Domain.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AccountingSystem.Application.Users
{
    public class AuthAppService : IAuthAppService
    {
        private readonly IConfiguration _configuration;
        private readonly IRepository<User, Guid> _userRepository;

        public AuthAppService(IConfiguration configuration, IRepository<User, Guid> userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }
        public async Task<string> Auth(LoginInput user)
        {
            var userInSystem = ( await _userRepository.GetQueryableAsync())
                .FirstOrDefault(c => c.Login == user.Login 
                                     && c.Password == user.Password);
            
            var handler = new JwtSecurityTokenHandler();
  
            var privateKey = Encoding.UTF8.GetBytes(_configuration["PrivateKey"]);
          
            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(privateKey),
                SecurityAlgorithms.HmacSha256);
  
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddDays(15),
                Subject = GenerateClaims(userInSystem)
            };
          
            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }
  
        private static ClaimsIdentity GenerateClaims(User user)
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