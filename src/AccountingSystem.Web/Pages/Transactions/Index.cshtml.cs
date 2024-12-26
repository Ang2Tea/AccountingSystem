using System.Transactions;
using AccountingSystem.Application.Contract.Transactions;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AccountingSystem.Web.Pages.Transactions;

public class Index(
    ITransactionAppService transactionAppService) : PageModel
{
    public List<TransactionDto> Transactions { get; set; } = [];
    
    public async Task OnGetAsync()
    {
        Transactions = await transactionAppService.GetListAsync();
    }
}