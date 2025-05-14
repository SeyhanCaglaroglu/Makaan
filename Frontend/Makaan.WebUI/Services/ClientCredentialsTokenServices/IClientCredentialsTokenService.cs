namespace Makaan.WebUI.Services.ClientCredentialsTokenServices
{
    public interface IClientCredentialsTokenService
    {
        Task<string> GetToken();
    }
}
