using Makaan.DtoLayer.CatalogDtos.FeaturedAboutDtos;
using Makaan.WebUI.Services.FeaturedAboutServices;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.WebUI.ViewComponents.DefaultViewComponents
{
    public class _AboutDefaultComponentPartial : ViewComponent
    {
        private readonly IFeaturedAboutService _featuredAboutService;

        public _AboutDefaultComponentPartial(IFeaturedAboutService featuredAboutService)
        {
            _featuredAboutService = featuredAboutService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var value = await _featuredAboutService.GetAllResultFeaturedAboutAsync();

            return View(value ?? new List<ResultFeaturedAboutDto>());
        }
    
    }
}
