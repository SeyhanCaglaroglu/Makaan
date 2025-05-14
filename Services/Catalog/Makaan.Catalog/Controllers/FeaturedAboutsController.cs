using Makaan.Catalog.Dtos.FeaturedAboutDtos;
using Makaan.Catalog.Services.FeaturedAboutServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.Catalog.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FeaturedAboutsController : ControllerBase
    {
        private readonly IFeaturedAboutService _featuredAboutService;

        public FeaturedAboutsController(IFeaturedAboutService featuredAboutService)
        {
            _featuredAboutService = featuredAboutService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllFeaturedAbout()
        {
            var value = await _featuredAboutService.GetAllResultFeaturedAboutAsync();
            return Ok(value);
        }
        [HttpPost]
        public async Task<IActionResult> CreateFeaturedAbout(CreateFeaturedAboutDto createFeaturedAboutDto)
        {

            await _featuredAboutService.CreateFeaturedAboutAsync(createFeaturedAboutDto);

            return Ok("Hakkımızda Öne Çıkan Alanı Başarıyla Eklendi !");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateFeaturedAbout(UpdateFeaturedAboutDto updateFeaturedAboutDto)
        {
            await _featuredAboutService.UpdateFeaturedAboutAsync(updateFeaturedAboutDto);
            return Ok("Hakkımızda Öne Çıkan Alanı Başarıyla Güncellendi!");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteFeaturedAbout(string id)
        {
            await _featuredAboutService.DeleteFeaturedAboutAsync(id);
            return Ok("Hakkımızda Öne Çıkan Alanı Başarıyla Silindi!");
        }
        [HttpGet]
        public async Task<IActionResult> GetByIdFeaturedAbout(string id)
        {
            var value = await _featuredAboutService.GetByIdFeaturedAboutAsync(id);
            return Ok(value);
        }
    }
}
