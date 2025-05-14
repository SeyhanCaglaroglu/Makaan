using Makaan.DtoLayer.CatalogDtos.PropertyIntroPosterDtos;
using Makaan.WebUI.Services.PropertyIntroPosterServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class PropertyIntroPosterController : Controller
    {
        private readonly IPropertyIntroPosterService _propertyIntroPosterService;

        public PropertyIntroPosterController(IPropertyIntroPosterService propertyIntroPosterService)
        {
            _propertyIntroPosterService = propertyIntroPosterService;
        }

        public async Task<IActionResult> Index()
        {

            var values = await _propertyIntroPosterService.GetAllResultPropertyIntroPosterAsync();

            if (values != null)
            {
                return View(values);
            }

            return View(new List<ResultPropertyIntroPosterDto>());
        }
        public IActionResult CreatePropertyIntroPoster()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreatePropertyIntroPoster(CreatePropertyIntroPosterDto createPropertyIntroPosterDto, IFormFile ImageURL)
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
                    createPropertyIntroPosterDto.PosterUrl = "/images/" + ImageURL.FileName;
                }

                await _propertyIntroPosterService.CreatePropertyIntroPosterAsync(createPropertyIntroPosterDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata meydana geldi: {ex.Message}");
                Console.WriteLine($"Detaylar: {ex.StackTrace}");
            }
            return Redirect("/Admin/PropertyIntroPoster/Index");
        }
        public async Task<IActionResult> UpdatePropertyIntroPoster(string id)
        {
            var value = await _propertyIntroPosterService.GetByIdPropertyIntroPosterAsync(id);
            return View(value);
        }
        [HttpPost]
        public async Task<IActionResult> UpdatePropertyIntroPoster(GetByIdPropertyIntroPosterDto getByIdPropertyIntroPosterDto, IFormFile ImageURL)
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
                    getByIdPropertyIntroPosterDto.PosterUrl = "/images/" + ImageURL.FileName;
                }

                await _propertyIntroPosterService.UpdatePropertyIntroPosterAsync(getByIdPropertyIntroPosterDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata meydana geldi: {ex.Message}");
                Console.WriteLine($"Detaylar: {ex.StackTrace}");
            }
            return Redirect("/Admin/PropertyIntroPoster/Index");
        }

        public async Task<IActionResult> DeletePropertyIntroPoster(string id)
        {
            await _propertyIntroPosterService.DeletePropertyIntroPosterAsync(id);
            return Redirect("/Admin/PropertyIntroPoster/Index");
        }
    }
}
