using Makaan.WebUI.Models;

namespace Makaan.WebUI.Services.SignInServices
{
    public interface ISignInService
    {
        Task<bool> SignInAsync(SignInViewModel signInViewModel);
    }
}
