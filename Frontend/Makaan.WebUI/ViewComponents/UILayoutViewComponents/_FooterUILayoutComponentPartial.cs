using Makaan.DtoLayer.CatalogDtos.ContactDtos;
using Makaan.WebUI.Services.ContactServices;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.WebUI.ViewComponents.UILayoutViewComponents
{
    public class _FooterUILayoutComponentPartial : ViewComponent
    {
        private readonly IContactService _contactService;

        public _FooterUILayoutComponentPartial(IContactService contactService)
        {
            _contactService = contactService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var value = await _contactService.GetAllResultContactAsync();

            return View(value ?? new List<ResultContactDto>());
        }
    }
}
