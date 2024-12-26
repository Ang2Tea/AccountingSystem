using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountingSystem.Application.Contract.ShopItems;
using AccountingSystem.Domain.Core;
using AccountingSystem.Domain.ShopItems;

namespace AccountingSystem.Application.ShopItems
{
    public class ShopItemsAppService : IShopItemAppService
    {
        private readonly IRepository<Category, Guid> _categoryRepository;
        private readonly IRepository<ShopItem, Guid> _shopItemRepository;

        public ShopItemsAppService(
            IRepository<Category, Guid> categoryRepository,
            IRepository<ShopItem, Guid> shopItemRepository)
        {
            _categoryRepository = categoryRepository;
            _shopItemRepository = shopItemRepository;
        }

        private static ShopItemDto MapShopItem(ShopItem shopItem)
        {
            var result = new ShopItemDto
            {
                Id = shopItem.Id,
                Name = shopItem.Name,
                Description = shopItem.Description,
                Price = shopItem.Price,
                Category = new CategoryDto()
                {
                    Id = shopItem.Category.Id,
                    Title = shopItem.Category.Title,
                    Description = shopItem.Category.Description,
                }
            };
            
            return result;
        }
        
        public async Task<ShopItemDto> CreateAsync(ChangeShopItemDto input)
        {
            var category = await _categoryRepository.GetByIdAsync(input.CategoryId);
            var newShopItem = new ShopItem(category, input.Name, input.Description, input.Price);
            
            await _shopItemRepository.CreateAsync(newShopItem);
            
            var result = MapShopItem(newShopItem);
            return result;
        }

        public async Task<List<ShopItemDto>> GetListAsync()
        {
            var shopItems = (await _shopItemRepository
                    .WithDetails(c => c.Category))
                    .ToList();
            
            var results = shopItems.Select(MapShopItem).ToList();
            return results;
        }

        public async Task<ShopItemDto> GetAsync(Guid id)
        {
            var shopItem =  (await _shopItemRepository
                .WithDetails(c => c.Category))
                .FirstOrDefault(c => c.Id == id);
            
            var result = MapShopItem(shopItem);
            return result;
        }

        public async Task<ShopItemDto> UpdateAsync(Guid id, ChangeShopItemDto input)
        {
            var shopItem = await _shopItemRepository.GetByIdAsync(id);
            var category = await _categoryRepository.GetByIdAsync(input.CategoryId);
            
            shopItem.Name = input.Name;
            shopItem.SetCategory(category);
            shopItem.Description = input.Description;
            shopItem.Price = input.Price;
            
            await _shopItemRepository.UpdateAsync(shopItem);

            var result = MapShopItem(shopItem);
            return result;
        }

        public Task DeleteAsync(Guid id)
        {
            return _shopItemRepository.DeleteAsync(id);
        }
    }
}