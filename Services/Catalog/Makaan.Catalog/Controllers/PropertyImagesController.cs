using Makaan.Catalog.Dtos.PropertyImageDtos;
using Makaan.Catalog.Services.PropertyImageServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.Catalog.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PropertyImagesController : ControllerBase
    {
        private readonly IPropertyImageService _propertyImageService;

        public PropertyImagesController(IPropertyImageService propertyImageService)
        {
            _propertyImageService = propertyImageService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPropertyImage()
        {
            var value = await _propertyImageService.GetAllResultPropertyImageAsync();
            return Ok(value);
        }
        [HttpPost]
        public async Task<IActionResult> CreatePropertyImage(CreatePropertyImageDto createPropertyImageDto)
        {

            await _propertyImageService.CreatePropertyImageAsync(createPropertyImageDto);

            return Ok("Mülk Görseli Başarıyla Eklendi !");
        }
        [HttpPut]
        public async Task<IActionResult> UpdatePropertyImage(UpdatePropertyImageDto updatePropertyImageDto)
        {
            await _propertyImageService.UpdatePropertyImageAsync(updatePropertyImageDto);
            return Ok("Mülk Görseli Başarıyla Güncellendi!");
        }
        [HttpDelete]
        public async Task<IActionResult> DeletePropertyImage(string id)
        {
            await _propertyImageService.DeletePropertyImageAsync(id);
            return Ok("Mülk Görseli Başarıyla Silindi!");
        }
        [HttpGet]
        public async Task<IActionResult> GetByIdPropertyImage(string id)
        {
            var value = await _propertyImageService.GetByIdPropertyImageAsync(id);
            return Ok(value);
        }
        [HttpGet]
        public async Task<IActionResult> GetByPropertyIdPropertyImage(string id)
        {
            var values = await _propertyImageService.GetByPropertyIdPropertyImageAsync(id);
            return Ok(values);
        }
    }
}
