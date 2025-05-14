using Makaan.Catalog.Dtos.IntroTextAreaDtos;
using Makaan.Catalog.Services.IntroTextAreaServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.Catalog.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IntroTextAreasController : ControllerBase
    {
        private readonly IIntroTextAreaService _introTextAreaService;

        public IntroTextAreasController(IIntroTextAreaService introTextAreaService)
        {
            _introTextAreaService = introTextAreaService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllIntroTextArea()
        {
            var values = await _introTextAreaService.GetAllIntroTextAreaAsync();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateIntroTextArea(CreateIntroTextAreaDto createIntroTextAreaDto)
        {

            await _introTextAreaService.CreateIntroTextAreaAsync(createIntroTextAreaDto);

            return Ok("Giriş Alanı Başarıyla Eklendi !");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateIntroTextArea(UpdateIntroTextAreaDto updateIntroTextAreaDto)
        {
            await _introTextAreaService.UpdateIntroTextAreaAsync(updateIntroTextAreaDto);
            return Ok("Giriş Alanı Başarıyla Güncellendi!");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteIntroTextArea(string id)
        {
            await _introTextAreaService.DeleteIntroTextAreaAsync(id);
            return Ok("Giriş Alanı Başarıyla Silindi!");
        }
        [HttpGet]
        public async Task<IActionResult> GetByIdIntroTextArea(string id)
        {
            var value = await _introTextAreaService.GetByIdIntroTextAreaAsync(id);
            return Ok(value);
        }
    }
}
