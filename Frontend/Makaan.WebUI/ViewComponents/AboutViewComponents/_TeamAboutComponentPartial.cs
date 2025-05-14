using Microsoft.AspNetCore.Mvc;

namespace Makaan.WebUI.ViewComponents.AboutViewComponents
{
    public class _TeamAboutComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    
    }
}
