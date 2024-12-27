using System.Threading.Tasks;

namespace AccountingSystem.Application.Contract.Users
{
    public interface IAuthAppService
    {
        Task<string> Auth(UserDto user);
    }
}