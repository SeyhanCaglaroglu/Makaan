using Makaan.WebUI.Models;

namespace Makaan.WebUI.Services.UserServices
{
    public interface IUserService
    {
        Task<string> GetUserRoleAsync(string Email, string Password);
        Task<string> GetUserRoleByEmail(string Email);
        Task UpdatePassword(string email, string newPassword);
    }
}
