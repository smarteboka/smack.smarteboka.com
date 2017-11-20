using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace smack.smarteboka.com.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        public IActionResult LogOut()
        {
            return new SignOutResult(new string[] { "oidc", "Cookies" });
        }
    }
}
