using Makaan.DtoLayer.CatalogDtos.PropertyAgentDtos;
using Makaan.WebUI.Services.PropertyAgentServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class PropertyAgentController : Controller
    {
        private readonly IPropertyAgentService _propertyAgentService;

        public PropertyAgentController(IPropertyAgentService propertyAgentService)
        {
            _propertyAgentService = propertyAgentService;
        }

        public async Task<IActionResult> Index()
        {

            var values = await _propertyAgentService.GetAllResultPropertyAgentAsync();

            if (values != null)
            {
                return View(values);
            }

            return View(new List<ResultPropertyAgentDto>());
        }
        public IActionResult CreatePropertyAgent()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreatePropertyAgent(CreatePropertyAgentDto createPropertyAgentDto, IFormFile ImageURL)
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
                    createPropertyAgentDto.ImageUrl = "/images/" + ImageURL.FileName;
                }

                await _propertyAgentService.CreatePropertyAgentAsync(createPropertyAgentDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata meydana geldi: {ex.Message}");
                Console.WriteLine($"Detaylar: {ex.StackTrace}");
            }

            return Redirect("/Admin/PropertyAgent/Index");
        }
        public async Task<IActionResult> UpdatePropertyAgent(string id)
        {
            var value = await _propertyAgentService.GetByIdPropertyAgentAsync(id);
            return View(value);
        }
        [HttpPost]
        public async Task<IActionResult> UpdatePropertyAgent(GetByIdPropertyAgentDto getByIdPropertyAgentDto, IFormFile ImageURL)
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
                    getByIdPropertyAgentDto.ImageUrl = "/images/" + ImageURL.FileName;
                }

                await _propertyAgentService.UpdatePropertyAgentAsync(getByIdPropertyAgentDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata meydana geldi: {ex.Message}");
                Console.WriteLine($"Detaylar: {ex.StackTrace}");
            }

            return Redirect("/Admin/PropertyAgent/Index");
        }

        public async Task<IActionResult> DeletePropertyAgent(string id)
        {
            await _propertyAgentService.DeletePropertyAgentAsync(id);
            return Redirect("/Admin/PropertyAgent/Index");
        }
    }
}
