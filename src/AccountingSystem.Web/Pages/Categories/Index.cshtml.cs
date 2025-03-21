using AccountingSystem.Application.Contract.ShopItems;
using AccountingSystem.Domain.ShopItems;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AccountingSystem.Web.Pages.Categories;

public class Index(ICategoryAppService categoryAppService) : PageModel
{
    public List<CategoryDto> Categories { get; set; } = [];
    
    public async Task OnGet()
    {
        Categories = await categoryAppService.GetListAsync() ?? [];
    }
    
    public async Task<IActionResult> OnGetDeleteAsync(Guid id)
    {
        await categoryAppService.DeleteAsync(id);
        
        return RedirectToPage("Index");
    }
}