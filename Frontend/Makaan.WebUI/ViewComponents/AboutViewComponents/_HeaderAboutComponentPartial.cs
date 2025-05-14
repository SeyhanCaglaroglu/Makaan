using Makaan.DtoLayer.CatalogDtos.FeaturedAboutDtos;
using Makaan.WebUI.Services.FeaturedAboutServices;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.WebUI.ViewComponents.AboutViewComponents
{
    public class _HeaderAboutComponentPartial : ViewComponent
    {
        private readonly IFeaturedAboutService _featuredAboutService;

        public _HeaderAboutComponentPartial(IFeaturedAboutService featuredAboutService)
        {
            _featuredAboutService = featuredAboutService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _featuredAboutService.GetAllResultFeaturedAboutAsync();

            return View(values ?? new List<ResultFeaturedAboutDto>());
        }
    }
}
