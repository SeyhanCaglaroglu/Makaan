using Makaan.WebUI.Services.PropertyServices;
using Makaan.WebUI.Services.PropertyTypeServices;
using Makaan.WebUI.Services.UserIdentityServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    [Area("Admin")]
    public class StatisticController : Controller
    {
        private readonly IPropertyService _propertyService;
        private readonly IPropertyTypeService _propertyTypeService;
        private readonly IUserIdentityService _userIdentityService;
        public StatisticController(IPropertyService propertyService, IPropertyTypeService propertyTypeService, IUserIdentityService userIdentityService)
        {
            _propertyService = propertyService;
            _propertyTypeService = propertyTypeService;
            _userIdentityService = userIdentityService;
        }

        public async Task<IActionResult> Index()
        {
            var properties = await _propertyService.GetAllResultPropertyAsync();//Mulkler

            var propertyTypes = await _propertyTypeService.GetAllPropertyTypeAsync();//Kategoriler

            var estateAgentUsers = await _userIdentityService.GetEstateAgentUsers(); //Emlakcilar

            var memberUsers = await _userIdentityService.GetMemberUsers();//Üyeler

            #region En yuksek ve En dusuk Kira degerini alma

            var forRents = properties.Where(x=>x.Status == "Kira").ToList();

            ViewBag.minPricedRent = forRents.Min(x => x.Price);
            ViewBag.highestPricedRent = forRents.Max(x => x.Price);
            #endregion

            #region En yüksek ve En düşük Satılık Değeri alma

            var forSales = properties.Where(x => x.Status == "Satılık").ToList();

            ViewBag.HighestPricedSale = forSales.Max(x => x.Price);
            ViewBag.MinPricedSale = forSales.Min(x => x.Price);

            #endregion

            #region Kategoriye Göre Mülk Sayısı

            var propertyCounts = new List<int>();

            foreach (var propertyType in propertyTypes)
            {
                var propertyCount = await _propertyService.GetPropertyCountByPropertyTypeId(propertyType.PropertyTypeId);
                propertyCounts.Add(propertyCount);
            }

            ViewData["propertyCounts"] = propertyCounts;
            ViewData["propertyTypes"] = propertyTypes;

            #endregion

            #region Toplam Degerler
                    
            ViewBag.PropertiesCount = properties.Count();
            ViewBag.PropertyTypesCount = propertyTypes.Count();
            ViewBag.EstateAgentCount = estateAgentUsers.Count();
            ViewBag.MemberUsers = memberUsers.Count();

            #endregion



            return View(propertyTypes);
        }
    }
}
