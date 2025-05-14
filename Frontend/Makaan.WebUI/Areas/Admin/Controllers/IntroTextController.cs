using Makaan.DtoLayer.CatalogDtos.IntroTextAreaDtos;
using Makaan.WebUI.Services.IntroTextAreaServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class IntroTextController : Controller
    {
        private readonly IIntroTextAreaService _introTextAreaService;

        public IntroTextController(IIntroTextAreaService introTextAreaService)
        {
            _introTextAreaService = introTextAreaService;
        }

        public async Task<IActionResult> Index()
        {

            var values = await _introTextAreaService.GetAllIntroTextAreaAsync();

            if (values != null)
            {
                return View(values);
            }

            return View(new List<ResultIntroTextAreaDto>());
        }
        public IActionResult CreateIntroText()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateIntroText(CreateIntroTextAreaDto createIntroTextAreaDto)
        {
            await _introTextAreaService.CreateIntroTextAreaAsync(createIntroTextAreaDto);
            return Redirect("/Admin/IntroText/Index");
        }
        public async Task<IActionResult> UpdateIntroText(string id)
        {
            var value = await _introTextAreaService.GetByIdIntroTextAreaAsync(id);
            return View(value);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateIntroText(GetByIdIntroTextAreaDto getByIdIntroTextAreaDto)
        {
            await _introTextAreaService.UpdateIntroTextAreaAsync(getByIdIntroTextAreaDto);
            return Redirect("/Admin/IntroText/Index");
        }

        public async Task<IActionResult> DeleteIntroText(string id)
        {
            await _introTextAreaService.DeleteIntroTextAreaAsync(id);
            return Redirect("/Admin/IntroText/Index"); 
        }
    }
}
