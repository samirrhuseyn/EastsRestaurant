using Microsoft.AspNetCore.Mvc;

namespace WebUI.ViewComponents.UILayoutComponent
{
    public class _UILayoutFooterComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
