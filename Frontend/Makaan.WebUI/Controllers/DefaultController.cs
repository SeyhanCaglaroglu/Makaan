using Makaan.DtoLayer.CatalogDtos.PropertyTypeDtos;
using Makaan.WebUI.Services.PropertyTypeServices;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.WebUI.Controllers
{
    public class DefaultController : Controller
    {
        private readonly IPropertyTypeService _propertyTypeService;

        public DefaultController(IPropertyTypeService propertyTypeService)
        {
            _propertyTypeService = propertyTypeService;
        }

        public async Task<IActionResult> Index()
        {
           

            var propertyTypes = await _propertyTypeService.GetAllPropertyTypeAsync();

            return View(propertyTypes ?? new List<ResultPropertyTypeDto>());
        }

        public async Task<IActionResult> SearchFilter()
        {
            var propertyTypes = await _propertyTypeService.GetAllPropertyTypeAsync();

            return PartialView(propertyTypes ?? new List<ResultPropertyTypeDto>());
        }
    }
}
