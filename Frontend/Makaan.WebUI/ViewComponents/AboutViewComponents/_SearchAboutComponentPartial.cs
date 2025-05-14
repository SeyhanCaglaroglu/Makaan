using Microsoft.AspNetCore.Mvc;

namespace Makaan.WebUI.ViewComponents.AboutViewComponents
{
    public class _SearchAboutComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
