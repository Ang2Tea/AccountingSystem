using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountingSystem.Application.Contract.ShopItems;
using AccountingSystem.Domain.Core;
using AccountingSystem.Domain.ShopItems;

namespace AccountingSystem.Application.ShopItems
{
    public class CategoryAppService : ICategoryAppService
    {
        private readonly IRepository<Category, Guid> _categoryRepository;

        public CategoryAppService(IRepository<Category, Guid> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        private static CategoryDto MapCategory(Category category)
        {
            var result = new CategoryDto
            {
                Id = category.Id,
                Title = category.Title,
                Description = category.Description,
            };
            
            return result;
        }
        
        public async Task<CategoryDto> CreateAsync(ChangeCategoryDto input)
        {
            var newCategory = new Category(input.Title, input.Description);
            await _categoryRepository.CreateAsync(newCategory);
            
            var result = MapCategory(newCategory);
            return result;
        }

        public async Task<List<CategoryDto>> GetListAsync()
        {
            var category = await _categoryRepository.GetAllAsync();
            var result = category
                .Select(MapCategory)
                .ToList();
            
            return result;
        }

        public async Task<CategoryDto> GetCategoryAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            
            var result = MapCategory(category);
            return result;
        }

        public async Task<CategoryDto> UpdateAsync(Guid id, ChangeCategoryDto input)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            category.Title = input.Title;
            category.Description = input.Description;
            
            await _categoryRepository.UpdateAsync(category);
            
            var result = MapCategory(category);
            return result;
        }

        public Task DeleteAsync(Guid id)
        {
            return _categoryRepository.DeleteAsync(id);
        }
    }
}