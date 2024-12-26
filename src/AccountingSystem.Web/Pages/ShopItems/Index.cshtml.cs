using AccountingSystem.Application.Contract.ShopItems;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AccountingSystem.Web.Pages.ShopItems;

public class Index(
    IShopItemAppService shopItemAppService
) : PageModel
{
    public List<ShopItemDto> ShopItems { get; set; } = [];

    public async Task OnGet()
    {
        ShopItems = await shopItemAppService.GetListAsync();
    }

    public async Task<IActionResult> OnGetDeleteAsync(Guid id)
    {
        await shopItemAppService.DeleteAsync(id);
        
        return RedirectToPage("Index");
    }
}