using Microsoft.AspNetCore.Mvc;

namespace Makaan.WebUI.Areas.Admin.ViewComponents.AdminLayoutViewComponents
{
    public class _HeadAdminLayoutComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
