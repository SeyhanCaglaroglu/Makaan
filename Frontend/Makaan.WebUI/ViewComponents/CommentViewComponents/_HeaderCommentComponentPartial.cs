using Makaan.DtoLayer.CatalogDtos.ContactIntroPosterDtos;
using Makaan.WebUI.Services.ContactIntroPosterServices;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.WebUI.ViewComponents.CommentViewComponents
{
    public class _HeaderCommentComponentPartial : ViewComponent
    {
        private readonly IContactIntroPosterService _contactIntroPosterService;

        public _HeaderCommentComponentPartial(IContactIntroPosterService contactIntroPosterService)
        {
            _contactIntroPosterService = contactIntroPosterService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _contactIntroPosterService.GetAllResultContactIntroPosterAsync();
            return View(values ?? new List<ResultContactIntroPosterDto>());
        }
    }
}
