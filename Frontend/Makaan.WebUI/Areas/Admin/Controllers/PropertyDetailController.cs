using Makaan.DtoLayer.CatalogDtos.PropertyDetailDtos;
using Makaan.WebUI.Services.PropertyDetailServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,EstateAgent")]
    [Area("Admin")]
    public class PropertyDetailController : Controller
    {
        private readonly IPropertyDetailService _propertyDetailService;

        public PropertyDetailController(IPropertyDetailService propertyDetailService)
        {
            _propertyDetailService = propertyDetailService;
        }

        public async Task<IActionResult> Index(string id)
        {
            ViewBag.Id = id;

            var values = await _propertyDetailService.GetByPropertyIdPropertyDetailAsync(id);

            if (values != null)
            {
                return View(values);
            }

            return View(new List<ResultPropertyDetailDto>());
        }
        public IActionResult CreatePropertyDetail(string id)
        {
            ViewBag.Id = id;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreatePropertyDetail(CreatePropertyDetailDto createPropertyDetailDto)
        {
            await _propertyDetailService.CreatePropertyDetailAsync(createPropertyDetailDto);
            return Redirect($"/Admin/PropertyDetail/Index?id={createPropertyDetailDto.PropertyId}");
        }

        public async Task<IActionResult> UpdatePropertyDetail(string id)
        {
            var value = await _propertyDetailService.GetByIdPropertyDetailAsync(id);
            return View(value);
        }
        [HttpPost]
        public async Task<IActionResult> UpdatePropertyDetail(GetByIdPropertyDetailDto getByIdPropertyDetailDto)
        {
            await _propertyDetailService.UpdatePropertyDetailAsync(getByIdPropertyDetailDto);
            return Redirect($"/Admin/PropertyDetail/Index?id={getByIdPropertyDetailDto.PropertyId}");
        }

        public async Task<IActionResult> DeletePropertyDetail(string id)
        {
            var propertyDetail = await _propertyDetailService.GetByIdPropertyDetailAsync(id);
            var PropertyId = propertyDetail.PropertyId;

            await _propertyDetailService.DeletePropertyDetailAsync(id);
            return Redirect($"/Admin/PropertyDetail/Index?id={PropertyId}");
        }
    }
}
