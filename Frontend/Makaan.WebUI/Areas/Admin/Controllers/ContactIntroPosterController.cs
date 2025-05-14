using Makaan.DtoLayer.CatalogDtos.ContactIntroPosterDtos;
using Makaan.WebUI.Services.ContactIntroPosterServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class ContactIntroPosterController : Controller
    {
        private readonly IContactIntroPosterService _contactIntroPosterService;

        public ContactIntroPosterController(IContactIntroPosterService contactIntroPosterService)
        {
            _contactIntroPosterService = contactIntroPosterService;
        }

        public async Task<IActionResult> Index()
        {

            var values = await _contactIntroPosterService.GetAllResultContactIntroPosterAsync();

            if (values != null)
            {
                return View(values);
            }

            return View(new List<ResultContactIntroPosterDto>());
        }
        public IActionResult CreateContactIntroPoster()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateContactIntroPoster(CreateContactIntroPosterDto createContactIntroPosterDto, IFormFile ImageURL)
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
                    createContactIntroPosterDto.PosterUrl = "/images/" + ImageURL.FileName;
                }

                await _contactIntroPosterService.CreateContactIntroPosterAsync(createContactIntroPosterDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata meydana geldi: {ex.Message}");
                Console.WriteLine($"Detaylar: {ex.StackTrace}");
            }
            return Redirect("/Admin/ContactIntroPoster/Index");
        }
        public async Task<IActionResult> UpdateContactIntroPoster(string id)
        {
            var value = await _contactIntroPosterService.GetByIdContactIntroPosterAsync(id);
            return View(value);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateContactIntroPoster(GetByIdContactIntroPosterDto getByIdContactIntroPosterDto, IFormFile ImageURL)
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
                    getByIdContactIntroPosterDto.PosterUrl = "/images/" + ImageURL.FileName;
                }

                await _contactIntroPosterService.UpdateContactIntroPosterAsync(getByIdContactIntroPosterDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata meydana geldi: {ex.Message}");
                Console.WriteLine($"Detaylar: {ex.StackTrace}");
            }
            return Redirect("/Admin/ContactIntroPoster/Index");
        }

        public async Task<IActionResult> DeleteContactIntroPoster(string id)
        {
            await _contactIntroPosterService.DeleteContactIntroPosterAsync(id);
            return Redirect("/Admin/ContactIntroPoster/Index");
        }
    }
}
