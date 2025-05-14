namespace Makaan.WebUI.Services.PasswordTokenServices
{
    public interface IPasswordTokenService
    {
        Task<string> GetAccessTokenWithRefreshToken();
    }
}
