using Makaan.DtoLayer.CatalogDtos.EstateAgentApplicationDtos;
using Makaan.DtoLayer.CatalogDtos.PropertyAgentDtos;
using Makaan.DtoLayer.CatalogDtos.PropertyDtos;
using Makaan.DtoLayer.CatalogDtos.PropertyImageDtos;
using Makaan.WebUI.Areas.Admin.Hubs;
using Makaan.WebUI.Services.EstateAgentApplicationServices;
using Makaan.WebUI.Services.PropertyAgentServices;
using Makaan.WebUI.Services.PropertyImageServices;
using Makaan.WebUI.Services.PropertyServices;
using Makaan.WebUI.Services.PropertyTypeServices;
using Makaan.WebUI.Services.UserIdentityServices;
using Makaan.WebUI.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace Makaan.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,EstateAgent")]
    [Area("Admin")]
    public class PropertyController : Controller
    {
        private readonly IPropertyService _propertyService;
        private readonly IPropertyTypeService _propertyTypeService;
        private readonly IPropertyAgentService _propertyAgentService;
        private readonly IPropertyImageService _propertyImageService;
        private readonly IUserIdentityService _userIdentityService;
        private readonly IUserService _userService;
        private readonly IEstateAgentApplicationService _estateAgentApplicationService;
        private readonly IHubContext<StatisticHub> _statisticHub;
        public PropertyController(IPropertyService propertyService, IPropertyTypeService propertyTypeService, IPropertyAgentService propertyAgentService, IPropertyImageService propertyImageService, IUserIdentityService userIdentityService, IUserService userService, IEstateAgentApplicationService estateAgentApplicationService, IHubContext<StatisticHub> statisticHub)
        {
            _propertyService = propertyService;
            _propertyTypeService = propertyTypeService;
            _propertyAgentService = propertyAgentService;
            _propertyImageService = propertyImageService;
            _userIdentityService = userIdentityService;
            _userService = userService;
            _estateAgentApplicationService = estateAgentApplicationService;
            _statisticHub = statisticHub;
        }

        public async Task<IActionResult> Index(string? city, int page=1)
        {
            var user = await _userIdentityService.GetUser();
            var userRole = await _userService.GetUserRoleByEmail(user.Email);

            ViewBag.UserRole = userRole;
            ViewBag.City = city;
            //Sistemdeki Emlakciyi Getirme
            user.UserName = Regex.Replace(user.UserName, "(?<!^)([A-Z])", " $1");

            GetByIdEstateAgentApplicationDto estateAgent = new();

            GetByIdPropertyAgentDto propertyAgent = new();

            if (userRole == "EstateAgent")
            {
                 estateAgent = await _estateAgentApplicationService.GetEstateAgentApplicationByAuthNameAsync(user.UserName);

                 propertyAgent = await _propertyAgentService.GetPropertyAgentByTitleAsync(estateAgent.CompanyName);
            }

            //Sistemdeki Kullaniciya Gore Mulkleri getirme
            var properties = userRole == "Admin" ? await _propertyService.GetAllResultPropertyAsync() : userRole == "EstateAgent" ? await _propertyService.GetPropertiesByPropertyAgentId(propertyAgent.PropertyAgentId) : null;

            //Mulkleri tersden siralayip sayfalama islemi yap
            if (properties != null)
            {
                //City Filtresi
                if(!string.IsNullOrEmpty(city))
                {
                    properties = properties.Where(x=>x.City.ToLower() == city.ToLower()).ToList();
                    if(properties.Count == 0)
                    {
                        TempData["zeroPropertiesMessage"] = "Aradığınız Sonuç bulunamadı !";
                    }
                }

                properties = properties.OrderByDescending(x => x.PropertyId).ToList();

                int pageSize = 5;

                var paginatedProperties = properties.Skip((page -1) * pageSize).Take(pageSize).ToList();

                ViewData["CurrentPage"] = page;
                ViewData["TotalPages"] = (int)Math.Ceiling(properties.Count / (double)pageSize);
                ViewData["pageSize"] = pageSize;

                return View(paginatedProperties);
            }

            return View(new List<ResultPropertyDto>());
        }
        public async Task<IActionResult> CreateProperty()
        {
            var user = await _userIdentityService.GetUser();
            var userRole = await _userService.GetUserRoleByEmail(user.Email);

            ViewBag.UserRole = userRole;

            //Sistemdeki Emlakciyi Getirme
            user.UserName = Regex.Replace(user.UserName, "(?<!^)([A-Z])", " $1");

            GetByIdEstateAgentApplicationDto estateAgent = new();

            GetByIdPropertyAgentDto propertyAgent = new();

            if (userRole == "EstateAgent")
            {
                estateAgent = await _estateAgentApplicationService.GetEstateAgentApplicationByAuthNameAsync(user.UserName);

                propertyAgent = await _propertyAgentService.GetPropertyAgentByTitleAsync(estateAgent.CompanyName);
            }

            ViewBag.propertyAgentId = propertyAgent.PropertyAgentId;

            var propertyTypes = await _propertyTypeService.GetAllPropertyTypeAsync();
            var propertyAgents = await _propertyAgentService.GetAllResultPropertyAgentAsync();

            List<SelectListItem> propertyTypeItems = (from item in propertyTypes
                                                      select new SelectListItem
                                                      {
                                                          Text = item.PropertyName,
                                                          Value = item.PropertyTypeId
                                                      }).ToList();

            List<SelectListItem> propertyAgentsItems = (from item in propertyAgents
                                                        select new SelectListItem
                                                        {
                                                            Text = item.Title,
                                                            Value = item.PropertyAgentId
                                                        }).ToList();

            ViewBag.propertyTypeItems = propertyTypeItems;
            ViewBag.propertyAgentItems = propertyAgentsItems;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateProperty(CreatePropertyDto createPropertyDto, IFormFile ImageURL)
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
                    createPropertyDto.ImageUrl = "/images/" + ImageURL.FileName;
                }

                await _propertyService.CreatePropertyAsync(createPropertyDto);

                //Guncel Mulk Sayisi
                await _statisticHub.Clients.All.SendAsync("ReceivePropertiesCountForAdmin", 1);

                //Guncel Fiyat
                var status = createPropertyDto.Status;
                var price = createPropertyDto.Price;
                await _statisticHub.Clients.All.SendAsync("ReceivePriceForAdmin", status,price);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata meydana geldi: {ex.Message}");
                Console.WriteLine($"Detaylar: {ex.StackTrace}");
            }

            return Redirect("/Admin/Property/Index");
        }
        public async Task<IActionResult> UpdateProperty(string id)
        {
            var user = await _userIdentityService.GetUser();
            var userRole = await _userService.GetUserRoleByEmail(user.Email);

            ViewBag.UserRole = userRole;

            //Sistemdeki Emlakciyi Getirme
            user.UserName = Regex.Replace(user.UserName, "(?<!^)([A-Z])", " $1");

            GetByIdEstateAgentApplicationDto estateAgent = new();

            GetByIdPropertyAgentDto propertyAgent = new();

            if (userRole == "EstateAgent")
            {
                estateAgent = await _estateAgentApplicationService.GetEstateAgentApplicationByAuthNameAsync(user.UserName);

                propertyAgent = await _propertyAgentService.GetPropertyAgentByTitleAsync(estateAgent.CompanyName);
            }

            ViewBag.propertyAgentId = propertyAgent.PropertyAgentId;

            var propertyTypes = await _propertyTypeService.GetAllPropertyTypeAsync();
            var propertyAgents = await _propertyAgentService.GetAllResultPropertyAgentAsync();

            List<SelectListItem> propertyTypeItems = (from item in propertyTypes
                                                      select new SelectListItem
                                                      {
                                                          Text = item.PropertyName,
                                                          Value = item.PropertyTypeId
                                                      }).ToList();

            List<SelectListItem> propertyAgentsItems = (from item in propertyAgents
                                                        select new SelectListItem
                                                        {
                                                            Text = item.Title,
                                                            Value = item.PropertyAgentId
                                                        }).ToList();

            ViewBag.propertyTypeItems = propertyTypeItems;
            ViewBag.propertyAgentItems = propertyAgentsItems;

            var value = await _propertyService.GetByIdPropertyAsync(id);
            return View(value);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProperty(GetByIdPropertyDto getByIdPropertyDto, IFormFile ImageURL,string CurrentPicture)
        {
            try
            {
                getByIdPropertyDto.ImageUrl = CurrentPicture;

                if (ImageURL != null && ImageURL.Length > 0)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", ImageURL.FileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        ImageURL.CopyTo(stream);
                    }
                    getByIdPropertyDto.ImageUrl = "/images/" + ImageURL.FileName;
                }

                await _propertyService.UpdatePropertyAsync(getByIdPropertyDto);

                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata meydana geldi: {ex.Message}");
                Console.WriteLine($"Detaylar: {ex.StackTrace}");
            }
            return Redirect("/Admin/Property/Index");
        }

        public async Task<IActionResult> DeleteProperty(string id)
        {
            await _propertyService.DeletePropertyAsync(id);

            //SignalR
            await _statisticHub.Clients.All.SendAsync("ReceivePropertiesCountForAdmin", -1);            

            return Redirect("/Admin/Property/Index");
        }
    }
}
