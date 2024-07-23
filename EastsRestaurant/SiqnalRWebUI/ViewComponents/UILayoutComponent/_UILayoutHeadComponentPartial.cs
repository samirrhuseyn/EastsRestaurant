using Microsoft.AspNetCore.Mvc;

namespace WebUI.ViewComponents.UILayoutComponent
{
    public class _UILayoutHeadComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
