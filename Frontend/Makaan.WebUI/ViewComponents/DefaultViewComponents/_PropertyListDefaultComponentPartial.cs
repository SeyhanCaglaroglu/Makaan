using Makaan.DtoLayer.CatalogDtos.PropertyDtos;
using Makaan.WebUI.Services.PropertyServices;
using Makaan.WebUI.Services.PropertyTypeServices;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.WebUI.ViewComponents.DefaultViewComponents
{
    public class _PropertyListDefaultComponentPartial : ViewComponent
    {
        private readonly IPropertyService _propertyService;
        private readonly IPropertyTypeService _propertyTypeService;
        public _PropertyListDefaultComponentPartial(IPropertyService propertyService, IPropertyTypeService propertyTypeService)
        {
            _propertyService = propertyService;
            _propertyTypeService = propertyTypeService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string? propertyTypeId, string? status, string? City,int page)
        {
            
            //PropertyTypeId filtresi
            var properties = propertyTypeId == null || propertyTypeId == "Tümü" ? await _propertyService.GetAllResultPropertyAsync() : await _propertyService.GetPropertiesByPropertyTypeId(propertyTypeId);


            //Durum Filtresi
            if (!string.IsNullOrEmpty(status) && status != "Tümü")
            {
                properties = properties.Where(x => x.Status == status).ToList();
            }

            //Sehir Filtresi
            if (!string.IsNullOrEmpty(City) && City != "Tümü")
            {
                properties = properties.Where(x => x.City == City).ToList();
            }

            //PropertyType i Id yerine name ini yazdirma
            var propertyTypes = await _propertyTypeService.GetAllPropertyTypeAsync();

            var propertyTypeDictionary = propertyTypes.ToDictionary(x => x.PropertyTypeId, x => x.PropertyName);

            ViewData["propertyTypeDictionary"] = propertyTypeDictionary;


            // Tersine çevirme işlemi
            properties = properties.OrderByDescending(x => x.PropertyId).ToList(); 

            //Pagination Islemleri
            int pageSize = 9;

            var paginatedProperties = properties.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = (int)Math.Ceiling(properties.Count / (double)pageSize);

            ViewBag.PropertyTypeId = propertyTypeId;
            ViewBag.Status = status;
            ViewBag.City = City;

            return View(paginatedProperties ?? new List<ResultPropertyDto>());
        }
    
    }
}
