using Makaan.Catalog.Dtos.EstateAgentApplicationDtos;
using Makaan.Catalog.Services.EstateAgentApplicationServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.Catalog.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EstateAgentApplicationsController : ControllerBase
    {
        private readonly IEstateAgentApplicationService _estateAgentApplicationService;

        public EstateAgentApplicationsController(IEstateAgentApplicationService estateAgentApplicationService)
        {
            _estateAgentApplicationService = estateAgentApplicationService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEstateAgentApplication()
        {
            var value = await _estateAgentApplicationService.GetAllResultEstateAgentApplicationAsync();
            return Ok(value);
        }
        [HttpPost]
        public async Task<IActionResult> CreateEstateAgentApplication(CreateEstateAgentApplicationDto createEstateAgentApplicationDto)
        {

            await _estateAgentApplicationService.CreateEstateAgentApplicationAsync(createEstateAgentApplicationDto);

            return Ok("Emlakçılık Başvurusu Başarıyla Eklendi !");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteEstateAgentApplication(string id)
        {
            await _estateAgentApplicationService.DeleteEstateAgentApplicationAsync(id);
            return Ok("Emlakçılık Başvurusu Başarıyla Silindi!");
        }
        [HttpGet]
        public async Task<IActionResult> GetByIdEstateAgentApplication(string id)
        {
            var value = await _estateAgentApplicationService.GetByIdEstateAgentApplicationAsync(id);
            return Ok(value);
        }
        [HttpGet]
        public async Task<IActionResult> GetEstateAgentApplicationByAuthName(string authName)
        {
            var value = await _estateAgentApplicationService.GetEstateAgentApplicationByAuthNameAsync(authName);
            return Ok(value);
        }
    }
}
