using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace smack.smarteboka.com.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult LogOut()
        {
            return new SignOutResult(new string[] { "oidc", "Cookies" });
        }
    }
}
