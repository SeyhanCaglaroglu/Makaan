using Makaan.DtoLayer.CatalogDtos.IntroSliderAreaDtos;
using Makaan.WebUI.Services.IntroSliderAreaServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class IntroSliderController : Controller
    {
        private readonly IIntroSliderAreaService _introSliderAreaService;

        public IntroSliderController(IIntroSliderAreaService introSliderAreaService)
        {
            _introSliderAreaService = introSliderAreaService;
        }

        public async Task<IActionResult> Index()
        {

            var values = await _introSliderAreaService.GetAllIntroSliderAreaAsync();

            if (values != null)
            {
                return View(values);
            }

            return View(new List<ResultIntroSliderAreaDto>());
        }
        public IActionResult CreateIntroSlider()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateIntroSlider(CreateIntroSliderAreaDto createIntroSliderAreaDto,IFormFile ImageURL)
        {
            try
            {
                if (ImageURL != null && ImageURL.Length > 0)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", ImageURL.FileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        ImageURL.CopyTo(stream);
                    }
                    createIntroSliderAreaDto.ImageUrl = "/images/" + ImageURL.FileName;
                }

                await _introSliderAreaService.CreateIntroSliderAreaAsync(createIntroSliderAreaDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata meydana geldi: {ex.Message}");
                Console.WriteLine($"Detaylar: {ex.StackTrace}");
            }


            return Redirect("/Admin/IntroSlider/Index");
            
        }
        public async Task<IActionResult> UpdateIntroSlider(string id)
        {
            var value = await _introSliderAreaService.GetByIdIntroSliderAreaAsync(id);
            return View(value);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateIntroSlider(GetByIdIntroSliderAreaDto getByIdIntroSliderAreaDto, IFormFile ImageURL)
        {
            try
            {
                if (ImageURL != null && ImageURL.Length > 0)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", ImageURL.FileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        ImageURL.CopyTo(stream);
                    }
                    getByIdIntroSliderAreaDto.ImageUrl = "/images/" + ImageURL.FileName;
                }

                await _introSliderAreaService.UpdateIntroSliderAreaAsync(getByIdIntroSliderAreaDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata meydana geldi: {ex.Message}");
                Console.WriteLine($"Detaylar: {ex.StackTrace}");
            }

            return Redirect("/Admin/IntroSlider/Index");
        }

        public async Task<IActionResult> DeleteIntroSlider(string id)
        {
            await _introSliderAreaService.DeleteIntroSliderAreaAsync(id);
            return Redirect("/Admin/IntroSlider/Index");
        }
    }
}
