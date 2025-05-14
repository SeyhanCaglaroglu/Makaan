using Makaan.Catalog.Dtos.PropertyIntroPosterDtos;
using Makaan.Catalog.Services.PropertyIntroPosterServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.Catalog.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PropertyIntroPostersController : ControllerBase
    {
        private readonly IPropertyIntroPosterService _PropertyIntroPosterService;

        public PropertyIntroPostersController(IPropertyIntroPosterService PropertyIntroPosterService)
        {
            _PropertyIntroPosterService = PropertyIntroPosterService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPropertyIntroPoster()
        {
            var value = await _PropertyIntroPosterService.GetAllResultPropertyIntroPosterAsync();
            return Ok(value);
        }
        [HttpPost]
        public async Task<IActionResult> CreatePropertyIntroPoster(CreatePropertyIntroPosterDto createPropertyIntroPosterDto)
        {

            await _PropertyIntroPosterService.CreatePropertyIntroPosterAsync(createPropertyIntroPosterDto);

            return Ok("Poster Başarıyla Eklendi !");
        }
        [HttpPut]
        public async Task<IActionResult> UpdatePropertyIntroPoster(UpdatePropertyIntroPosterDto updatePropertyIntroPosterDto)
        {
            await _PropertyIntroPosterService.UpdatePropertyIntroPosterAsync(updatePropertyIntroPosterDto);
            return Ok("Poster Başarıyla Güncellendi!");
        }
        [HttpDelete]
        public async Task<IActionResult> DeletePropertyIntroPoster(string id)
        {
            await _PropertyIntroPosterService.DeletePropertyIntroPosterAsync(id);
            return Ok("Poster Başarıyla Silindi!");
        }
        [HttpGet]
        public async Task<IActionResult> GetByIdPropertyIntroPoster(string id)
        {
            var value = await _PropertyIntroPosterService.GetByIdPropertyIntroPosterAsync(id);
            return Ok(value);
        }
    }
}
