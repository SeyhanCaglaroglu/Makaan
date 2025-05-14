using Microsoft.AspNetCore.Mvc;

namespace Makaan.WebUI.ViewComponents.ContactViewComponents
{
    public class _SearchContactComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
