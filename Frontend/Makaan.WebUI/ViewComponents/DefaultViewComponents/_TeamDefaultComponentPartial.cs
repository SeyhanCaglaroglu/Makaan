using Makaan.DtoLayer.CatalogDtos.PropertyAgentDtos;
using Makaan.WebUI.Services.PropertyAgentServices;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.WebUI.ViewComponents.DefaultViewComponents
{
    public class _TeamDefaultComponentPartial : ViewComponent
    {
        private readonly IPropertyAgentService _propertyAgentService;

        public _TeamDefaultComponentPartial(IPropertyAgentService propertyAgentService)
        {
            _propertyAgentService = propertyAgentService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var propertyAgents = await _propertyAgentService.GetAllResultPropertyAgentAsync();
            return View(propertyAgents ?? new List<ResultPropertyAgentDto>());
        }
    
    }
}
