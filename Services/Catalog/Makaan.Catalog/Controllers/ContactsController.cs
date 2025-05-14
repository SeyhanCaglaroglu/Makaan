using Makaan.Catalog.Dtos.ContactDtos;
using Makaan.Catalog.Services.ContactServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.Catalog.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")] // testtttttttt
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllContact()
        {
            var value = await _contactService.GetAllResultContactAsync();
            return Ok(value);
        }
        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateContactDto createContactDto)
        {

            await _contactService.CreateContactAsync(createContactDto);

            return Ok("İletişim Alanı Başarıyla Eklendi !");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateContact(UpdateContactDto updateContactDto)
        {
            await _contactService.UpdateContactAsync(updateContactDto);
            return Ok("İletişim Alanı Başarıyla Güncellendi!");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteContact(string id)
        {
            await _contactService.DeleteContactAsync(id);
            return Ok("İletişim Alanı Başarıyla Silindi!");
        }
        [HttpGet]
        public async Task<IActionResult> GetByIdContact(string id)
        {
            var value = await _contactService.GetByIdContactAsync(id);
            return Ok(value);
        }
    }
}
