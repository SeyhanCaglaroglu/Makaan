using Makaan.DtoLayer.CatalogDtos.PropertyTypeDtos;
using Makaan.WebUI.Areas.Admin.Hubs;
using Makaan.WebUI.Services.PropertyTypeServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Makaan.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class PropertyTypeController : Controller
    {
        private readonly IPropertyTypeService _propertyTypeService;
        private readonly IHubContext<StatisticHub> _statisticHub;
        public PropertyTypeController(IPropertyTypeService propertyTypeService, IHubContext<StatisticHub> statisticHub)
        {
            _propertyTypeService = propertyTypeService;
            _statisticHub = statisticHub;
        }

        public async Task<IActionResult> Index(string? propertyName, int page = 1)
        {
            // PropertyType verilerini çekiyoruz
            var values = await _propertyTypeService.GetAllPropertyTypeAsync();

            // Eğer propertyName varsa, filtreleme yapıyoruz
            if (!string.IsNullOrEmpty(propertyName))
            {
                values = values.Where(x => x.PropertyName.Contains(propertyName, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            int pageSize = 5;

            // Sayfalama işlemi
            var paginatedPropertyTypes = values.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // ViewData ile sayfa bilgilerini gönderiyoruz
            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = (int)Math.Ceiling(values.Count / (double)pageSize);
            ViewData["pageSize"] = pageSize;

            if(values != null)
            {
                return View(paginatedPropertyTypes);
            }

            return View(new List<ResultPropertyTypeDto>());
        }

        public IActionResult CreatePropertyType()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreatePropertyType(CreatePropertyTypeDto createPropertyTypeDto, IFormFile ImageURL)
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
                    createPropertyTypeDto.PropertyIconUrl = "/images/" + ImageURL.FileName;
                }

                await _propertyTypeService.CreatePropertyTypeAsync(createPropertyTypeDto);

                //Property Type sayisini canli dinleme
                await _statisticHub.Clients.All.SendAsync("ReceivePropertyTypeCountForAdmin", 1);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata meydana geldi: {ex.Message}");
                Console.WriteLine($"Detaylar: {ex.StackTrace}");
            }
            return Redirect("/Admin/PropertyType/Index");
        }
        public async Task<IActionResult> UpdatePropertyType(string id)
        {
            var value = await _propertyTypeService.GetByIdPropertyTypeAsync(id);
            return View(value);
        }
        [HttpPost]
        public async Task<IActionResult> UpdatePropertyType(GetByIdPropertyTypeDto getByIdPropertyTypeDto,IFormFile ImageURL)
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
                    getByIdPropertyTypeDto.PropertyIconUrl = "/images/" + ImageURL.FileName;
                }

                await _propertyTypeService.UpdatePropertyTypeAsync(getByIdPropertyTypeDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata meydana geldi: {ex.Message}");
                Console.WriteLine($"Detaylar: {ex.StackTrace}");
            }
            return Redirect("/Admin/PropertyType/Index");
        }

        public async Task<IActionResult> DeletePropertyType(string id)
        {
            await _propertyTypeService.DeletePropertyTypeAsync(id);

            //Property Type sayisini canli dinleme
            await _statisticHub.Clients.All.SendAsync("ReceivePropertyTypeCountForAdmin", -1);

            return Redirect("/Admin/PropertyType/Index");
        }
    }
}
