using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using AccountingSystem.Application.Contract.Users;
using AccountingSystem.Domain.Core;
using AccountingSystem.Domain.Users;

namespace AccountingSystem.Application.Users
{
    public class UserAppService : IUserAppService
    {
        private readonly IConfiguration _configuration;
        private readonly IRepository<User, Guid> _userRepository;

        public UserAppService(
            IConfiguration configuration,
            IRepository<User, Guid> userRepository)
        {
            _configuration = configuration; 
            _userRepository = userRepository;
        }

        private static UserDto MapToUserDto(User user)
        {
            var result = new UserDto
            {
                Id = user.Id,
                Login = user.Login,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                IsAdmin = user.IsAdmin
            };
            
            return result;
        }
        
        public async Task<UserDto> CreateAsync(ChangeUserDto input)
        {
            var salt = _configuration["StringEncryption:Salt"];
            var newUser = new User(input.Login, input.Password, salt, input.Email, input.Name, input.Surname);
            
            await _userRepository.CreateAsync(newUser);
            return MapToUserDto(newUser);
        }

        public async Task<List<UserDto>> GetListAsync()
        {
            var users = await _userRepository.GetAllAsync();
            var result = users
                .Select(MapToUserDto)
                .ToList();
            
            return result;
        }

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            
            var result = MapToUserDto(user);
            return result;
        }

        public Task<UserDto> UpdateAsync(Guid id, ChangeUserDto input)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _userRepository.DeleteAsync(id);
        }
    }
}