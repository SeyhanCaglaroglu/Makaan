using Makaan.Catalog.Dtos.PropertyAgentDtos;
using Makaan.Catalog.Services.PropertyAgentServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.Catalog.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PropertyAgentsController : ControllerBase
    {
        private readonly IPropertyAgentService _propertyAgentService;

        public PropertyAgentsController(IPropertyAgentService propertyAgentService)
        {
            _propertyAgentService = propertyAgentService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPropertyAgent()
        {
            var value = await _propertyAgentService.GetAllResultPropertyAgentAsync();
            return Ok(value);
        }
        [HttpPost]
        public async Task<IActionResult> CreatePropertyAgent(CreatePropertyAgentDto createPropertyAgentDto)
        {

            await _propertyAgentService.CreatePropertyAgentAsync(createPropertyAgentDto);

            return Ok("Emlak Ajansı Başarıyla Eklendi !");
        }
        [HttpPut]
        public async Task<IActionResult> UpdatePropertyAgent(UpdatePropertyAgentDto updatePropertyAgentDto)
        {
            await _propertyAgentService.UpdatePropertyAgentAsync(updatePropertyAgentDto);
            return Ok("Emlak Ajansı Başarıyla Güncellendi!");
        }
        [HttpDelete]
        public async Task<IActionResult> DeletePropertyAgent(string id)
        {
            await _propertyAgentService.DeletePropertyAgentAsync(id);
            return Ok("Emlak Ajansı Başarıyla Silindi!");
        }
        [HttpGet]
        public async Task<IActionResult> GetByIdPropertyAgent(string id)
        {
            var value = await _propertyAgentService.GetByIdPropertyAgentAsync(id);
            return Ok(value);
        }
        [HttpGet]
        public async Task<IActionResult> GetPropertyAgentByTitle(string title)
        {
            var value = await _propertyAgentService.GetPropertyAgentByTitleAsync(title);
            return Ok(value);
        }
    }
}
