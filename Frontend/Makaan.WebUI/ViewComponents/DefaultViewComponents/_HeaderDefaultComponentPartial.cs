using Makaan.DtoLayer.CatalogDtos.IntroSliderAreaDtos;
using Makaan.DtoLayer.CatalogDtos.IntroTextAreaDtos;
using Makaan.WebUI.Models;
using Makaan.WebUI.Services.IntroSliderAreaServices;
using Makaan.WebUI.Services.IntroTextAreaServices;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.WebUI.ViewComponents.DefaultViewComponents
{
    public class _HeaderDefaultComponentPartial : ViewComponent
    {
        private readonly IIntroTextAreaService _introTextAreaService;
        private readonly IIntroSliderAreaService _introSliderAreaService;
        public _HeaderDefaultComponentPartial(IIntroTextAreaService introTextAreaService, IIntroSliderAreaService introSliderAreaService)
        {
            _introTextAreaService = introTextAreaService;
            _introSliderAreaService = introSliderAreaService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var introText = await _introTextAreaService.GetAllIntroTextAreaAsync();

            var introSlider = await _introSliderAreaService.GetAllIntroSliderAreaAsync();

            var headerViewModel = new HeaderViewModel
            {
                resultIntroTextAreaDto = introText ?? new List<ResultIntroTextAreaDto>(),
                resultSliderTextAreaDto = introSlider ?? new List<ResultIntroSliderAreaDto>()
            };

            return View(headerViewModel);
        }
    }
}
