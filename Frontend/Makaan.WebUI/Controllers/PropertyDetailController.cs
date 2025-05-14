using Makaan.WebUI.Models;
using Makaan.WebUI.Services.PropertyAgentServices;
using Makaan.WebUI.Services.PropertyDetailServices;
using Makaan.WebUI.Services.PropertyImageServices;
using Makaan.WebUI.Services.PropertyServices;
using Makaan.WebUI.Services.PropertyTypeServices;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.WebUI.Controllers
{
    public class PropertyDetailController : Controller
    {
        private readonly IPropertyService _propertyService;
        private readonly IPropertyDetailService _propertyDetailService;
        private readonly IPropertyImageService _propertyImageService;
        private readonly IPropertyAgentService _propertyAgentService;
        private readonly IPropertyTypeService _propertyTypeService;
        public PropertyDetailController(IPropertyService propertyService, IPropertyDetailService propertyDetailService, IPropertyImageService propertyImageService, IPropertyAgentService propertyAgentService, IPropertyTypeService propertyTypeService)
        {
            _propertyService = propertyService;
            _propertyDetailService = propertyDetailService;
            _propertyImageService = propertyImageService;
            _propertyAgentService = propertyAgentService;
            _propertyTypeService = propertyTypeService;
        }

        public async Task<IActionResult> Index(string id)
        {
            var property = await _propertyService.GetByIdPropertyAsync(id);

            var propertyDetails = await _propertyDetailService.GetByPropertyIdPropertyDetailAsync(id);

            var propertyImages = await _propertyImageService.GetByPropertyIdPropertyImageAsync(id);

            PropertyViewModel model = new PropertyViewModel
            {
                getByIdPropertyDto = property,
                resultPropertyDetailDto = propertyDetails,
                resultPropertyImageDto = propertyImages
            };

            //id degeri girilen emlakcinin ismini gosterme dictionary
            var propertyAgents = await _propertyAgentService.GetAllResultPropertyAgentAsync();

            var propertyAgentDictionary = propertyAgents.ToDictionary(x => x.PropertyAgentId, x => x.Title);

            ViewData["propertyAgentDictionary"] = propertyAgentDictionary;


            //id degeri girilen mulk tipinin ismini gosterme dictionary
            var propertyTypes = await _propertyTypeService.GetAllPropertyTypeAsync();

            var propertyTypeDictionary = propertyTypes.ToDictionary(x => x.PropertyTypeId, x => x.PropertyName);

            ViewData["propertyTypeDictionary"] = propertyTypeDictionary;

            return View(model);
        }
        [HttpPost]
        public IActionResult Index(DateTime selectedDate,string Id)
        {
            return Redirect($"/PropertyDetail/Index?id={Id}");
        }
    }
}
