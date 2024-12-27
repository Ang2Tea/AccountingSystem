using AccountingSystem.Application.Contract.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AccountingSystem.Web.Pages.Auth;

public class Registration(IUserAppService userAppService) : PageModel
{
    [BindProperty]
    public ChangeUserDto User { get; set; } = new();
    
    public void OnGet()
    {
        
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var user = await userAppService.CreateAsync(User);
        
        return RedirectToPage("../Index");
    }
}