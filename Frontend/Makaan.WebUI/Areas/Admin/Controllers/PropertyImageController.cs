using Makaan.DtoLayer.CatalogDtos.PropertyImageDtos;
using Makaan.WebUI.Services.PropertyImageServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,EstateAgent")]
    [Area("Admin")]
    public class PropertyImageController : Controller
    {
        private readonly IPropertyImageService _propertyImageService;

        public PropertyImageController(IPropertyImageService PropertyImageService)
        {
            _propertyImageService = PropertyImageService;
        }

        public async Task<IActionResult> Index(string id)
        {
            ViewBag.Id = id;

            var values = await _propertyImageService.GetByPropertyIdPropertyImageAsync(id);

            if (values != null)
            {
                return View(values);
            }

            return View(new List<ResultPropertyImageDto>());
        }
        public IActionResult CreatePropertyImage(string id)
        {
            ViewBag.Id = id;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreatePropertyImage(CreatePropertyImageDto createPropertyImageDto, IFormFile ImageURL)
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
                    createPropertyImageDto.ImageUrl = "/images/" + ImageURL.FileName;
                }

                await _propertyImageService.CreatePropertyImageAsync(createPropertyImageDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata meydana geldi: {ex.Message}");
                Console.WriteLine($"Detaylar: {ex.StackTrace}");
            }


            return Redirect($"/Admin/PropertyImage/Index?id={createPropertyImageDto.PropertyId}");

        }
        public async Task<IActionResult> UpdatePropertyImage(string id)
        {
            var value = await _propertyImageService.GetByIdPropertyImageAsync(id);
            return View(value);
        }
        [HttpPost]
        public async Task<IActionResult> UpdatePropertyImage(GetByIdPropertyImageDto getByIdPropertyImageDto, IFormFile ImageURL)
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
                    getByIdPropertyImageDto.ImageUrl = "/images/" + ImageURL.FileName;
                }

                await _propertyImageService.UpdatePropertyImageAsync(getByIdPropertyImageDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata meydana geldi: {ex.Message}");
                Console.WriteLine($"Detaylar: {ex.StackTrace}");
            }

            return Redirect($"/Admin/PropertyImage/Index?id={getByIdPropertyImageDto.PropertyId}");
        }

        public async Task<IActionResult> DeletePropertyImage(string id)
        {
            var propertyImage = await _propertyImageService.GetByIdPropertyImageAsync(id);
            var PropertyId = propertyImage.PropertyId;

            await _propertyImageService.DeletePropertyImageAsync(id);
            return Redirect($"/Admin/PropertyImage/Index?id={PropertyId}");
        }
    }
}
