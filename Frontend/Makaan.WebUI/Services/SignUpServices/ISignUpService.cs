using Makaan.WebUI.Models;

namespace Makaan.WebUI.Services.SignUpServices
{
    public interface ISignUpService
    {
        Task CreateUserAsync(SaveUserViewModel saveUserViewModel);
    }
}
