
using IdentityModel.AspNetCore.AccessTokenManagement;
using IdentityModel.Client;
using Makaan.WebUI.Settings;
using Microsoft.Extensions.Options;

namespace Makaan.WebUI.Services.ClientCredentialsTokenServices
{
    public class ClientCredentialsTokenService : IClientCredentialsTokenService
    {
        private readonly HttpClient _httpClient;
        private readonly IClientAccessTokenCache _clientAccessTokenCache;
        private readonly ClientSettings _clientSettings;
        private readonly ServiceApiSettings _serviceApiSettings;
        public ClientCredentialsTokenService(HttpClient httpClient, IClientAccessTokenCache clientAccessTokenCache, IOptions<ClientSettings> clientSettings, IOptions<ServiceApiSettings> serviceApiSettings)
        {
            _httpClient = httpClient;
            _clientAccessTokenCache = clientAccessTokenCache;
            _clientSettings = clientSettings.Value;
            _serviceApiSettings = serviceApiSettings.Value;
        }

        public async Task<string> GetToken()
        {
            var currentToken = await _clientAccessTokenCache.GetAsync("Makaan");

            if(currentToken != null)
            {
                return currentToken.AccessToken;
            }

            var discoveryEnpoint = await _httpClient.GetDiscoveryDocumentAsync(_serviceApiSettings.IdentityServerUrl);

            if(discoveryEnpoint.IsError)
            {
                throw new Exception($"DiscoveryEndpoint Alinamiyor !: {discoveryEnpoint.Error}");
            }

            var clientCredentialsTokenRequest = new ClientCredentialsTokenRequest
            {
                ClientId = _clientSettings.Makaan_Visitor.ClientId,
                ClientSecret = _clientSettings.Makaan_Visitor.ClientSecret,
                Address = discoveryEnpoint.TokenEndpoint
            };

            var newToken = await _httpClient.RequestClientCredentialsTokenAsync(clientCredentialsTokenRequest);

            if(newToken.IsError)
            {
                throw new Exception($"newToken Alinamiyor !: {newToken.Error}");
            }

            await _clientAccessTokenCache.SetAsync("Makaan",newToken.AccessToken,newToken.ExpiresIn);

            return newToken.AccessToken;
        }
    }
}
