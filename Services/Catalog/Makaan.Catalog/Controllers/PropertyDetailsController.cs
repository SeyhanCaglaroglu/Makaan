using Makaan.Catalog.Dtos.PropertyDetailDtos;
using Makaan.Catalog.Services.PropertyDetailServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.Catalog.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PropertyDetailsController : ControllerBase
    {
        private readonly IPropertyDetailService _propertyDetailService;

        public PropertyDetailsController(IPropertyDetailService propertyDetailService)
        {
            _propertyDetailService = propertyDetailService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPropertyDetail()
        {
            var value = await _propertyDetailService.GetAllResultPropertyDetailAsync();
            return Ok(value);
        }
        [HttpPost]
        public async Task<IActionResult> CreatePropertyDetail(CreatePropertyDetailDto createPropertyDetailDto)
        {

            await _propertyDetailService.CreatePropertyDetailAsync(createPropertyDetailDto);

            return Ok("Mülk Detayı Başarıyla Eklendi !");
        }
        [HttpPut]
        public async Task<IActionResult> UpdatePropertyDetail(UpdatePropertyDetailDto updatePropertyDetailDto)
        {
            await _propertyDetailService.UpdatePropertyDetailAsync(updatePropertyDetailDto);
            return Ok("Mülk Detayı Başarıyla Güncellendi!");
        }
        [HttpDelete]
        public async Task<IActionResult> DeletePropertyDetail(string id)
        {
            await _propertyDetailService.DeletePropertyDetailAsync(id);
            return Ok("Mülk Detayı Başarıyla Silindi!");
        }
        [HttpGet]
        public async Task<IActionResult> GetByIdPropertyDetail(string id)
        {
            var value = await _propertyDetailService.GetByIdPropertyDetailAsync(id);
            return Ok(value);
        }
        [HttpGet]
        public async Task<IActionResult> GetByPropertyIdPropertyDetail(string id)
        {
            var values = await _propertyDetailService.GetByPropertyIdPropertyDetailAsync(id);
            return Ok(values);
        }
    }
}
