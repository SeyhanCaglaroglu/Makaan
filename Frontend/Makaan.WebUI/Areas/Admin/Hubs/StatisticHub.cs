using Makaan.WebUI.Services.PropertyServices;
using Makaan.WebUI.Services.UserIdentityServices;
using Microsoft.AspNetCore.SignalR;

namespace Makaan.WebUI.Areas.Admin.Hubs
{
    public class StatisticHub : Hub<IStatisticHub>
    {
        private readonly IPropertyService _propertyService;

        public StatisticHub(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        public async Task BroadCastPropertiesPriceToAdmin()
        {
            var properties = await _propertyService.GetAllResultPropertyAsync();//Mulkler

            await Clients.All.ReceivePropertiesPriceForAdmin();
        }
    }
}
