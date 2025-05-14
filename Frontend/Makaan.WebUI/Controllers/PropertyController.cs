using Makaan.WebUI.Services.PropertyServices;
using Makaan.WebUI.Services.PropertyTypeServices;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.WebUI.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IPropertyTypeService _propertyTypeService;
        private readonly IPropertyService _propertyService;
        public PropertyController(IPropertyTypeService propertyTypeService, IPropertyService propertyService)
        {
            _propertyTypeService = propertyTypeService;
            _propertyService = propertyService;
        }

        public IActionResult Index(string? propertyTypeId, string? status, string? City, int page=1)
        {
            ViewBag.PropertyTypeId = propertyTypeId;
            ViewBag.Status = status;
            ViewBag.City = City;
            ViewBag.Page = page;
            
            return View();
        }
    }
}
