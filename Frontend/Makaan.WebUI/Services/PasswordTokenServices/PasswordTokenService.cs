
using IdentityModel.Client;
using Makaan.WebUI.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Makaan.WebUI.Services.PasswordTokenServices
{
    public class PasswordTokenService : IPasswordTokenService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ServiceApiSettings _serviceApiSettings;
        private readonly ClientSettings _clientSettings;
        public PasswordTokenService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IOptions<ServiceApiSettings> serviceApiSettings, IOptions<ClientSettings> clientSettings)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _serviceApiSettings = serviceApiSettings.Value;
            _clientSettings = clientSettings.Value;
        }

        public async Task<string> GetAccessTokenWithRefreshToken()
        {
            // Discovery endpoint'i al
            var discoveryEndpoint = await _httpClient.GetDiscoveryDocumentAsync(_serviceApiSettings.IdentityServerUrl);

            if (discoveryEndpoint.IsError || string.IsNullOrEmpty(discoveryEndpoint.TokenEndpoint))
            {
                throw new Exception("DiscoveryEndpoint eksik veya hatalı !");
            }

            // Refresh token'ı al
            var refreshToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);

            if (string.IsNullOrEmpty(refreshToken))
            {
                throw new Exception("Refresh token alınamadı !");
            }

            // Kullanıcının rolünü al (örneğin Admin veya Member)
            var userRole = _httpContextAccessor.HttpContext.User?.FindFirst("role")?.Value;

            if (string.IsNullOrEmpty(userRole))
            {
                throw new Exception("Kullanıcı rolü bulunamadı !");
            }

            // Admin veya Member için farklı client bilgilerini alıyoruz
            var ClientIdValue = "";
            var ClientSecretValue = "";

            // Kullanıcının rolüne göre uygun Client'ı seç
            if (userRole == "Admin")
            {
                ClientIdValue = _clientSettings.Makaan_Admin.ClientId;
                ClientSecretValue = _clientSettings.Makaan_Admin.ClientSecret;
            }
            else if (userRole == "Member")
            {
                ClientIdValue = _clientSettings.Makaan_Member.ClientId;
                ClientSecretValue = _clientSettings.Makaan_Member.ClientSecret;
            }
            else
            {
                throw new Exception("Geçersiz kullanıcı rolü !");
            }

            // Client bilgilerini kullanıcı rolüne göre ayarlıyoruz
            var refreshTokenRequest = new RefreshTokenRequest
            {
                ClientId = ClientIdValue,
                ClientSecret = ClientSecretValue,
                RefreshToken = refreshToken,
                Address = discoveryEndpoint.TokenEndpoint
            };

            // Refresh token ile yeni access token almak
            var token = await _httpClient.RequestRefreshTokenAsync(refreshTokenRequest);

            if (token.IsError || string.IsNullOrEmpty(token.AccessToken))
            {
                throw new Exception("Token alma hatası !");
            }

            // Yeni token bilgilerini oturum özelliklerine ekleyelim
            var authenticationToken = new List<AuthenticationToken>
    {
        new AuthenticationToken
        {
            Name = OpenIdConnectParameterNames.AccessToken,
            Value = token.AccessToken
        },
        new AuthenticationToken
        {
            Name = OpenIdConnectParameterNames.RefreshToken,
            Value = token.RefreshToken
        },
        new AuthenticationToken
        {
            Name = OpenIdConnectParameterNames.ExpiresIn,
            Value = DateTime.Now.AddSeconds(token.ExpiresIn).ToString()
        }
    };

            // Kullanıcı oturumunu güncelle
            var result = await _httpContextAccessor.HttpContext.AuthenticateAsync();
            var properties = result.Properties;

            // Yeni token bilgilerini oturum özelliklerine (properties) ekle
            properties.StoreTokens(authenticationToken);

            // Güncellenmiş token bilgileriyle kullanıcı oturumunu tekrar güncelle
            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, result.Principal, properties);

            return token.AccessToken;
        }

    }
}
