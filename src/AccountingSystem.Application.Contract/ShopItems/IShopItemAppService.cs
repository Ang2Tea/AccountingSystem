using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccountingSystem.Application.Contract.Users;

namespace AccountingSystem.Application.Contract.ShopItems
{
    public interface IShopItemAppService
    {
        Task<ShopItemDto> CreateAsync(ChangeShopItemDto input);
        Task<List<ShopItemDto>> GetListAsync();
        Task<ShopItemDto> GetAsync(Guid id);
        Task<ShopItemDto> UpdateAsync(Guid id, ChangeShopItemDto input);
        Task DeleteAsync(Guid id);
    }
}