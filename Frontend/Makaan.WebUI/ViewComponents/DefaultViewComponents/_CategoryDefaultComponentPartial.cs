using Makaan.DtoLayer.CatalogDtos.PropertyTypeDtos;
using Makaan.WebUI.Services.PropertyServices;
using Makaan.WebUI.Services.PropertyTypeServices;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.WebUI.ViewComponents.DefaultViewComponents
{
    public class _CategoryDefaultComponentPartial : ViewComponent
    {
        private readonly IPropertyTypeService _propertyTypeService;
        private readonly IPropertyService _propertyService;
        public _CategoryDefaultComponentPartial(IPropertyTypeService propertyTypeService, IPropertyService propertyService)
        {
            _propertyTypeService = propertyTypeService;
            _propertyService = propertyService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var propertyTypes = await _propertyTypeService.GetAllPropertyTypeAsync();

            var propertyCounts = new List<int>();

            foreach (var propertyType in propertyTypes)
            {
                var propertyCount = await _propertyService.GetPropertyCountByPropertyTypeId(propertyType.PropertyTypeId);
                propertyCounts.Add(propertyCount);
            }

            ViewData["propertyCounts"] = propertyCounts;

            return View(propertyTypes ?? new List<ResultPropertyTypeDto>());
        }
    
    }
}
