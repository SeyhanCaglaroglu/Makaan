using Makaan.Catalog.Dtos.PropertyDtos;
using Makaan.Catalog.Services.PropertyServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.Catalog.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertyService _propertyService;
        public PropertiesController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProperty()
        {
            var value = await _propertyService.GetAllResultPropertyAsync();
            return Ok(value);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProperty(CreatePropertyDto createPropertyDto)
        {

            await _propertyService.CreatePropertyAsync(createPropertyDto);

            return Ok("Mülk Başarıyla Eklendi !");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProperty(UpdatePropertyDto updatePropertyDto)
        {
            await _propertyService.UpdatePropertyAsync(updatePropertyDto);
            return Ok("Mülk Başarıyla Güncellendi!");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProperty(string id)
        {
            await _propertyService.DeletePropertyAsync(id);
            return Ok("Mülk Başarıyla Silindi!");
        }
        [HttpGet]
        public async Task<IActionResult> GetByIdProperty(string id)
        {
            var value = await _propertyService.GetByIdPropertyAsync(id);
            return Ok(value);
        }
        [HttpGet]
        public async Task<IActionResult> GetPropertiesByPropertyTypeId(string id)
        {
            var value = await _propertyService.GetPropertiesByPropertyTypeId(id);
            return Ok(value);
        }
        [HttpGet]
        public async Task<IActionResult> GetPropertyCountByPropertyTypeId(string id)
        {
            var value = await _propertyService.GetPropertyCountByPropertyTypeId(id);
            return Ok(value);
        }
        [HttpGet]
        public async Task<IActionResult> GetPropertiesByPropertyAgentId(string id)
        {
            var value = await _propertyService.GetPropertiesByPropertyAgentId(id);
            return Ok(value);
        }
    }
}
