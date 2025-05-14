using Makaan.DtoLayer.CatalogDtos.FeaturedAboutDtos;
using Makaan.WebUI.Services.FeaturedAboutServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class FeaturedAboutController : Controller
    {
        private readonly IFeaturedAboutService _featuredAboutService;

        public FeaturedAboutController(IFeaturedAboutService featuredAboutService)
        {
            _featuredAboutService = featuredAboutService;
        }

        public async Task<IActionResult> Index()
        {

            var values = await _featuredAboutService.GetAllResultFeaturedAboutAsync();

            if (values != null)
            {
                return View(values);
            }

            return View(new List<ResultFeaturedAboutDto>());
        }
        public IActionResult CreateFeaturedAbout()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateFeaturedAbout(CreateFeaturedAboutDto createFeaturedAboutDto, IFormFile ImageURL, IFormFile PosterURL)
        {
            try
            {
                // ImageURL dosyasını yükleme işlemi
                if (ImageURL != null && ImageURL.Length > 0)
                {
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", ImageURL.FileName);

                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await ImageURL.CopyToAsync(stream);
                    }
                    createFeaturedAboutDto.ImageUrl = "/images/" + ImageURL.FileName;
                }

                // PosterURL dosyasını yükleme işlemi
                if (PosterURL != null && PosterURL.Length > 0)
                {
                    var posterPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", PosterURL.FileName);

                    using (var stream = new FileStream(posterPath, FileMode.Create))
                    {
                        await PosterURL.CopyToAsync(stream);
                    }
                    createFeaturedAboutDto.IntroPosterUrl = "/images/" + PosterURL.FileName;
                }

                // Servis metodunu çağırma
                await _featuredAboutService.CreateFeaturedAboutAsync(createFeaturedAboutDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata meydana geldi: {ex.Message}");
                Console.WriteLine($"Detaylar: {ex.StackTrace}");
            }

            return Redirect("/Admin/FeaturedAbout/Index");
        }

        public async Task<IActionResult> UpdateFeaturedAbout(string id)
        {
            var value = await _featuredAboutService.GetByIdFeaturedAboutAsync(id);
            return View(value);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateFeaturedAbout(GetByIdFeaturedAboutDto getByIdFeaturedAboutDto, IFormFile ImageURL, IFormFile PosterURL)
        {
            try
            {
                // ImageURL dosyasını yükleme işlemi
                if (ImageURL != null && ImageURL.Length > 0)
                {
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", ImageURL.FileName);

                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await ImageURL.CopyToAsync(stream);
                    }
                    getByIdFeaturedAboutDto.ImageUrl = "/images/" + ImageURL.FileName;
                }

                // PosterURL dosyasını yükleme işlemi
                if (PosterURL != null && PosterURL.Length > 0)
                {
                    var posterPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", PosterURL.FileName);

                    using (var stream = new FileStream(posterPath, FileMode.Create))
                    {
                        await PosterURL.CopyToAsync(stream);
                    }
                    getByIdFeaturedAboutDto.IntroPosterUrl = "/images/" + PosterURL.FileName;
                }

                // Servis metodunu çağırma
                await _featuredAboutService.UpdateFeaturedAboutAsync(getByIdFeaturedAboutDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata meydana geldi: {ex.Message}");
                Console.WriteLine($"Detaylar: {ex.StackTrace}");
            }

            return Redirect("/Admin/FeaturedAbout/Index");
        }

        public async Task<IActionResult> DeleteFeaturedAbout(string id)
        {
            await _featuredAboutService.DeleteFeaturedAboutAsync(id);
            return Redirect("/Admin/FeaturedAbout/Index");
        }
    }
}
