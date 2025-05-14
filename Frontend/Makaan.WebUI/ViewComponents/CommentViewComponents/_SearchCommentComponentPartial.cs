using Microsoft.AspNetCore.Mvc;

namespace Makaan.WebUI.ViewComponents.CommentViewComponents
{
    public class _SearchCommentComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
