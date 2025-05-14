using Makaan.Catalog.Dtos.ContactIntroPosterDtos;
using Makaan.Catalog.Services.ContactIntroPosterServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.Catalog.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ContactIntroPostersController : ControllerBase
    {
        private readonly IContactIntroPosterService _contactIntroPosterService;

        public ContactIntroPostersController(IContactIntroPosterService contactIntroPosterService)
        {
            _contactIntroPosterService = contactIntroPosterService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllContactIntroPoster()
        {
            var value = await _contactIntroPosterService.GetAllResultContactIntroPosterAsync();
            return Ok(value);
        }
        [HttpPost]
        public async Task<IActionResult> CreateContactIntroPoster(CreateContactIntroPosterDto createContactIntroPosterDto)
        {

            await _contactIntroPosterService.CreateContactIntroPosterAsync(createContactIntroPosterDto);

            return Ok("Poster Başarıyla Eklendi !");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateContactIntroPoster(UpdateContactIntroPosterDto updateContactIntroPosterDto)
        {
            await _contactIntroPosterService.UpdateContactIntroPosterAsync(updateContactIntroPosterDto);
            return Ok("Poster Başarıyla Güncellendi!");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteContactIntroPoster(string id)
        {
            await _contactIntroPosterService.DeleteContactIntroPosterAsync(id);
            return Ok("Poster Başarıyla Silindi!");
        }
        [HttpGet]
        public async Task<IActionResult> GetByIdContactIntroPoster(string id)
        {
            var value = await _contactIntroPosterService.GetByIdContactIntroPosterAsync(id);
            return Ok(value);
        }
    }
}
