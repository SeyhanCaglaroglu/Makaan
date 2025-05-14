using Microsoft.AspNetCore.Mvc;

namespace Makaan.WebUI.ViewComponents.UILayoutViewComponents
{
    public class _SpinnerUILayoutComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
