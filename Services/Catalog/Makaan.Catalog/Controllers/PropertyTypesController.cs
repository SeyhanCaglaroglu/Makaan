using Makaan.Catalog.Dtos.PropertyTypeDtos;
using Makaan.Catalog.Services.PropertyTypeServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.Catalog.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PropertyTypesController : ControllerBase
    {
        private readonly IPropertyTypeService _propertyTypeService;
        public PropertyTypesController(IPropertyTypeService propertyTypeService)
        {
            _propertyTypeService = propertyTypeService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPropertyType()
        {
            var value = await _propertyTypeService.GetAllResultPropertyTypeAsync();
            return Ok(value);
        }
        [HttpPost]
        public async Task<IActionResult> CreatePropertyType(CreatePropertyTypeDto createPropertyTypeDto)
        {

            await _propertyTypeService.CreatePropertyTypeAsync(createPropertyTypeDto);

            return Ok("Mülk Tipi Başarıyla Eklendi !");
        }
        [HttpPut]
        public async Task<IActionResult> UpdatePropertyType(UpdatePropertyTypeDto updatePropertyTypeDto)
        {
            await _propertyTypeService.UpdatePropertyTypeAsync(updatePropertyTypeDto);
            return Ok("Mülk Tipi Başarıyla Güncellendi!");
        }
        [HttpDelete]
        public async Task<IActionResult> DeletePropertyType(string id)
        {
            await _propertyTypeService.DeletePropertyTypeAsync(id);
            return Ok("Mülk Tipi Başarıyla Silindi!");
        }
        [HttpGet]
        public async Task<IActionResult> GetByIdPropertyType(string id)
        {
            var value = await _propertyTypeService.GetByIdPropertyTypeAsync(id);
            return Ok(value);
        }
    }
}
