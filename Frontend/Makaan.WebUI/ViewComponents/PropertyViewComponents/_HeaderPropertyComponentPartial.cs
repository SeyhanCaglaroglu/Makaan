using Makaan.DtoLayer.CatalogDtos.PropertyIntroPosterDtos;
using Makaan.WebUI.Services.PropertyIntroPosterServices;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.WebUI.ViewComponents.PropertyIntroPosterViewComponents
{
    public class _HeaderPropertyComponentPartial : ViewComponent
    {
        private readonly IPropertyIntroPosterService _propertyIntroPosterService;

        public _HeaderPropertyComponentPartial(IPropertyIntroPosterService propertyIntroPosterService)
        {
            _propertyIntroPosterService = propertyIntroPosterService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var properties = await _propertyIntroPosterService.GetAllResultPropertyIntroPosterAsync();

            return View(properties ?? new List<ResultPropertyIntroPosterDto>());
        }
    }
}
