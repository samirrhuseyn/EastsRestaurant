using Microsoft.AspNetCore.Mvc;

namespace WebUI.ViewComponents.UILayoutComponent
{
    public class _UILayoutScriptsComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
