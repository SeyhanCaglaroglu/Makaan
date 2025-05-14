using Microsoft.AspNetCore.Mvc;

namespace Makaan.WebUI.ViewComponents.AboutViewComponents
{
    public class _FeatureAboutComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    
    }
}
