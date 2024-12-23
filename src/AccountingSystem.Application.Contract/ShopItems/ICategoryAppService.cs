using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountingSystem.Application.Contract.ShopItems
{
    public interface ICategoryAppService
    {
        Task<CategoryDto> CreateAsync(ChangeCategoryDto input);
        Task<List<CategoryDto>> GetListAsync();
        Task<CategoryDto> GetCategoryAsync(Guid id);
        Task<CategoryDto> UpdateAsync(Guid id, ChangeCategoryDto input);
        Task DeleteAsync(Guid id);
    }
}