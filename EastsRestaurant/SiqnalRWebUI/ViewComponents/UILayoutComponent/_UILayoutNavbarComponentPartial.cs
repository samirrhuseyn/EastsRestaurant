using Microsoft.AspNetCore.Mvc;

namespace WebUI.ViewComponents.UILayoutComponent
{
    public class _UILayoutNavbarComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
