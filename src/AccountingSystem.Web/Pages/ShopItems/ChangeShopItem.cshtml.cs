using AccountingSystem.Application.Contract.ShopItems;
using AccountingSystem.Domain.ShopItems;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AccountingSystem.Web.Pages.ShopItems;

public class ChangeShopItem(
    IShopItemAppService shopItemAppService,
    ICategoryAppService categoryAppService
) : PageModel
{
    [BindProperty] public ShopItemView ShopItem { get; set; } = new();
    public List<CategoryDto> Categories { get; set; } = [];

    public async Task OnGet(Guid id)
    {
        Categories = await categoryAppService.GetListAsync();
        if (id == Guid.Empty) return;

        var shopItem = await shopItemAppService.GetAsync(id);
        ShopItem = new ShopItemView
        {
            Id = shopItem.Id,
            CategoryId = shopItem.Category.Id,
            Category = shopItem.Category,
            Name = shopItem.Name,
            Description = shopItem.Description,
            Price = shopItem.Price,
        };
    }

    public async Task<IActionResult> OnPost()
    {
        var isCreate = ShopItem.Id == Guid.Empty;
        var changeShopItem = new ChangeShopItemDto
        {
            CategoryId = ShopItem.CategoryId,
            Name = ShopItem.Name,
            Description = ShopItem.Description,
            Price = ShopItem.Price,
        };
        
        var result = isCreate ?  shopItemAppService.CreateAsync(changeShopItem) : shopItemAppService.UpdateAsync(ShopItem.Id, changeShopItem);
        await result;

        return RedirectToPage("Index");
    }
}

public class ShopItemView
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public CategoryDto Category { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}