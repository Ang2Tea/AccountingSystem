using AccountingSystem.Application.Contract.ShopItems;
using AccountingSystem.Application.Contract.Transactions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AccountingSystem.Web.Pages.Transactions;

public class CreateTransaction(
    IShopItemAppService shopItemAppService,
    ITransactionAppService transactionAppService) : PageModel
{
    public TransactionView Transaction { get; set; } = new();
    public List<ShopItemDto> ShopItems { get; set; } = [];
    
    public async Task OnGetAsync()
    {
        ShopItems = await shopItemAppService.GetListAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var newTransaction = new CreateTransactionInput
        {
            ShopItemId = Transaction.ShopItemId,
            Type = (TransactionType)Transaction.Type,
            Amount = Transaction.Amount,
        };
        
        await transactionAppService.AddTransaction(newTransaction);

        return RedirectToPage("Index");
    }
}

public class TransactionView
{
    public Guid ShopItemId { get; set; }
    public int Type { get; set; }
    public int Amount { get; set; }
}