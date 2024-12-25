using AccountingSystem.Application.Contract.ShopItems;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AccountingSystem.Web.Pages.ShopItems;

public class Index(
    IShopItemAppService shopItemAppService,
    ICategoryAppService categoryAppService) : PageModel
{
    public List<ShopItemDto> ShopItems { get; set; } = [];

    public ChangeShopItemDto ChangeShopItem { get; set; } = new();
    
    public List<CategoryDto> Categories { get; set; }= [];
    
    public async Task OnGet()
    {
        Categories = await categoryAppService.GetListAsync();
        ShopItems = await shopItemAppService.GetListAsync();
    }
}