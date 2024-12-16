using AccountingSystem.Application.Contract.Users;
using Microsoft.AspNetCore.Mvc;

namespace AccountingSystem.Web.Controllers;

public class UserController(IUserAppService userAppService) : Controller
{
    // GET
    public async Task<IActionResult> Index()
    {
        return Json(await userAppService.GetListAsync());
    }
}