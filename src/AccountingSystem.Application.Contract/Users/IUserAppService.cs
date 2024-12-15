using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountingSystem.Application.Contract.Users
{
    public interface IUserAppService
    {
        Task<UserDto> CreateAsync(ChangeUserDto input);
        Task<List<UserDto>> GetListAsync();
        Task<UserDto> GetByIdAsync(Guid id);
        Task<UserDto> UpdateAsync(ChangeUserDto input);
        Task DeleteAsync(Guid id);
    }
}