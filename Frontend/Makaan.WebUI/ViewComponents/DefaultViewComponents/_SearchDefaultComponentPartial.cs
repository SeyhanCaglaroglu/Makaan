using Makaan.DtoLayer.CatalogDtos.PropertyTypeDtos;
using Makaan.WebUI.Services.PropertyTypeServices;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.WebUI.ViewComponents.DefaultViewComponents
{
    public class _SearchDefaultComponentPartial : ViewComponent
    {
        private readonly IPropertyTypeService _propertyTypeService;

        public _SearchDefaultComponentPartial(IPropertyTypeService propertyTypeService)
        {
            _propertyTypeService = propertyTypeService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var propertyTypes = await _propertyTypeService.GetAllPropertyTypeAsync();

            return View(propertyTypes ?? new List<ResultPropertyTypeDto>());
        }
    
    }
}
