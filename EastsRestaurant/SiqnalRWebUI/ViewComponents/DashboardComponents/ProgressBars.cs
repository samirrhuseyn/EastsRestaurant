using Microsoft.AspNetCore.Mvc;

namespace WebUI.ViewComponents.DashboardComponents
{
    public class ProgressBars : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
