using Makaan.DtoLayer.CatalogDtos.ContactDtos;
using Makaan.WebUI.Services.ContactServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles="Admin")]
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        public async Task<IActionResult> Index()
        {

            var values = await _contactService.GetAllResultContactAsync();

            if (values != null)
            {
                return View(values);
            }

            return View(new List<ResultContactDto>());
        }
        public IActionResult CreateContact()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateContactDto createContactDto, IFormFile ImageURL)
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
                    createContactDto.IntroPosterUrl = "/images/" + ImageURL.FileName;
                }

                await _contactService.CreateContactAsync(createContactDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata meydana geldi: {ex.Message}");
                Console.WriteLine($"Detaylar: {ex.StackTrace}");
            }
            return Redirect("/Admin/Contact/Index");
        }
        public async Task<IActionResult> UpdateContact(string id)
        {
            var value = await _contactService.GetByIdContactAsync(id);
            return View(value);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateContact(GetByIdContactDto getByIdContactDto, IFormFile ImageURL)
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
                    getByIdContactDto.IntroPosterUrl = "/images/" + ImageURL.FileName;
                }

                await _contactService.UpdateContactAsync(getByIdContactDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata meydana geldi: {ex.Message}");
                Console.WriteLine($"Detaylar: {ex.StackTrace}");
            }
            return Redirect("/Admin/Contact/Index");
        }

        public async Task<IActionResult> DeleteContact(string id)
        {
            await _contactService.DeleteContactAsync(id);
            return Redirect("/Admin/Contact/Index");
        }
    }
}
