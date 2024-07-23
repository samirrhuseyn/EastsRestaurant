using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        public IActionResult Index(int code)
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
