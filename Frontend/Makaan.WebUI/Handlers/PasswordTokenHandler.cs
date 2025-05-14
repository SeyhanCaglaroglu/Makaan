using Makaan.WebUI.Services.PasswordTokenServices;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Net.Http.Headers;
using System.Net;
using Microsoft.AspNetCore.Authentication;

namespace Makaan.WebUI.Handlers
{
    public class PasswordTokenHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPasswordTokenService _passwordTokenService;
        public PasswordTokenHandler(IHttpContextAccessor httpContextAccessor, IPasswordTokenService passwordTokenService)
        {
            _httpContextAccessor = httpContextAccessor;
            _passwordTokenService = passwordTokenService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // HttpContext kontrolü
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                throw new UnauthorizedAccessException("HTTP context is missing.");
            }

            var accessToken = await httpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new UnauthorizedAccessException("Access token is missing.");
            }

            // İlk istek, access token ile yapılır
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await base.SendAsync(request, cancellationToken);

            // Eğer response 401 (Unauthorized) dönerse, token'ı yenileyip tekrar dene
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var newAccessToken = await _passwordTokenService.GetAccessTokenWithRefreshToken();

                if (string.IsNullOrEmpty(newAccessToken))
                {
                    throw new UnauthorizedAccessException("Failed to retrieve a new access token using the refresh token.");
                }

                // Yenilenen access token ile yeniden istek yap
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newAccessToken);
                response = await base.SendAsync(request, cancellationToken);
            }

            // Yeniden 401 hatası alırsak, oturumun geçersiz olduğunu belirten hata fırlat
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException("Unauthorized access after token refresh attempt.");
            }

            return response;
        }
    }
}
