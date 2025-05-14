using Makaan.Catalog.Dtos.IntroSliderAreaDtos;
using Makaan.Catalog.Services.IntroSliderAreaServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.Catalog.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IntroSliderAreasController : ControllerBase
    {
        private readonly IIntroSliderAreaService _introSliderAreaService;

        public IntroSliderAreasController(IIntroSliderAreaService introSliderAreaService)
        {
            _introSliderAreaService = introSliderAreaService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllIntroSliderArea()
        {
            var sliders = await _introSliderAreaService.GetAllResultIntroSliderAreaAsync();
            return Ok(sliders);
        }
        [HttpPost]
        public async Task<IActionResult> CreateIntroSliderArea(CreateIntroSliderAreaDto createIntroSliderAreaDto)
        {

            await _introSliderAreaService.CreateIntroSliderAreaAsync(createIntroSliderAreaDto);

            return Ok("Giriş Slaytı Başarıyla Eklendi !");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateIntroSliderArea(UpdateIntroSliderAreaDto updateIntroSliderAreaDto)
        {
            await _introSliderAreaService.UpdateIntroSliderAreaAsync(updateIntroSliderAreaDto);
            return Ok("Giriş Slaytı Başarıyla Güncellendi!");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteIntroSliderArea(string id)
        {
            await _introSliderAreaService.DeleteIntroSliderAreaAsync(id);
            return Ok("Giriş Slaytı Başarıyla Silindi!");
        }
        [HttpGet]
        public async Task<IActionResult> GetByIdIntroSliderArea(string id)
        {
            var value = await _introSliderAreaService.GetByIdIntroSliderAreaAsync(id);
            return Ok(value);
        }
    }
}
