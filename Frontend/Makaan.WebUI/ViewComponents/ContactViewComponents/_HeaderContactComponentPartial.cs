using Makaan.DtoLayer.CatalogDtos.ContactDtos;
using Makaan.WebUI.Services.ContactServices;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.WebUI.ViewComponents.ContactViewComponents
{
    public class _HeaderContactComponentPartial : ViewComponent
    {
        private readonly IContactService _contactService;

        public _HeaderContactComponentPartial(IContactService contactService)
        {
            _contactService = contactService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _contactService.GetAllResultContactAsync();
            return View(values ?? new List<ResultContactDto>());
        }
    }
}
