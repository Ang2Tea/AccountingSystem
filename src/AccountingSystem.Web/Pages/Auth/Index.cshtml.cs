using AccountingSystem.Application.Contract.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AccountingSystem.Web.Pages.Auth;

public class Index(IAuthAppService authAppService) : PageModel
{
    [BindProperty]
    public LoginInput Input { get; set; } = new();
    
    public void OnGet()
    {
        
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var token = await authAppService.Auth(Input);
        return RedirectToPage("/Index");
    }
}