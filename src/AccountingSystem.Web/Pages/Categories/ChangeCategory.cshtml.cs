using AccountingSystem.Application.Contract.ShopItems;
using AccountingSystem.Domain.ShopItems;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AccountingSystem.Web.Pages.Categories;

public class ChangeCategory(ICategoryAppService categoryAppService) : PageModel
{
    [BindProperty] public CategoryView Category { get; set; } = new();
    public async Task OnGetAsync(Guid id)
    {
        if (id == Guid.Empty) return;
        
        var category = await categoryAppService.GetCategoryAsync(id);
        Category = new CategoryView
        {
            Id = category.Id,
            Title = category.Title,
            Description = category.Description,
        };
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var isCreate = Category.Id == Guid.Empty;
        var changeCategory = new ChangeCategoryDto()
        {
            Title = Category.Title,
            Description = Category.Description,
        };
        
        var resultTask = isCreate ? categoryAppService.CreateAsync(changeCategory) : categoryAppService.UpdateAsync(Category.Id, changeCategory);
        
        await resultTask;
        return RedirectToPage("Index");
    }
}

public class CategoryView
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}