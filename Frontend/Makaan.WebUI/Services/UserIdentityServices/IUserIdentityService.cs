using Makaan.WebUI.Areas.Admin.Models;
using Makaan.WebUI.Models;

namespace Makaan.WebUI.Services.UserIdentityServices
{
    public interface IUserIdentityService
    {
        Task<UserDetailViewModel> GetUser();
        Task<List<UserDetailViewModel>> GetAllUsers();
        Task<List<UserDetailViewModel>> GetEstateAgentUsers();
        Task<List<UserDetailViewModel>> GetMemberUsers();
        Task UpdateRole(UpdateRoleViewModel updateRoleViewModel);
        Task DeleteUser(string email);
        Task CreateEstateAgent(SaveUserViewModel saveUserViewModel);
        
    }
}
