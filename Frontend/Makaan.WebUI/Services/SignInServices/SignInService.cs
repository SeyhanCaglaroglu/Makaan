using IdentityModel.Client;
using Makaan.WebUI.Models;
using Makaan.WebUI.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Globalization;
using System.Security.Claims;
using Makaan.WebUI.Services.UserServices;

namespace Makaan.WebUI.Services.SignInServices
{
    public class SignInService : ISignInService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ServiceApiSettings _serviceApiSettings;
        private readonly ClientSettings _clientSettings;
        private readonly IUserService _userService;
        public SignInService(IHttpContextAccessor httpContextAccessor, HttpClient httpClient, IOptions<ServiceApiSettings> serviceApiSettings, IOptions<ClientSettings> clientSettings, IUserService userService)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClient = httpClient;
            _serviceApiSettings = serviceApiSettings.Value;
            _clientSettings = clientSettings.Value;
            _userService = userService;
        }

        public async Task<bool> SignInAsync(SignInViewModel signInViewModel)
        {
            var discoveryEndpoint = await _httpClient.GetDiscoveryDocumentAsync(_serviceApiSettings.IdentityServerUrl);

            if(discoveryEndpoint.IsError || string.IsNullOrEmpty(discoveryEndpoint.TokenEndpoint))
            {
                throw new Exception("DiscoveryEndpoint eksik veya hatali !");
            }
            //Kullanici rolune gore token ayari
            var userRole = await _userService.GetUserRoleAsync(signInViewModel.Email,signInViewModel.Password);

            if(string.IsNullOrEmpty(userRole))
            {
                return false;
            }

            string ClientIdValue = "";
            string ClientSecretValue = "";

            ClientIdValue = userRole == "Admin" ? _clientSettings.Makaan_Admin.ClientId : userRole == "Member" ? _clientSettings.Makaan_Member.ClientId : _clientSettings.Makaan_EstateAgent.ClientId;

            ClientSecretValue = userRole == "Admin" ? _clientSettings.Makaan_Admin.ClientSecret : userRole == "Member" ? _clientSettings.Makaan_Member.ClientSecret : _clientSettings.Makaan_EstateAgent.ClientSecret;


            var passwordTokenRequest = new PasswordTokenRequest
            {
                ClientId = ClientIdValue,
                ClientSecret = ClientSecretValue,
                UserName = signInViewModel.Email,
                Password = signInViewModel.Password,
                Address = discoveryEndpoint.TokenEndpoint
            };

            var token = await _httpClient.RequestPasswordTokenAsync(passwordTokenRequest);

            if(token.IsError || string.IsNullOrEmpty(token.AccessToken))
            {
                //throw new Exception("Kullanici bilgileri eksik veya hatali !");
                return false;
            }

            var userInfoRequest = new UserInfoRequest
            {
                Token = token.AccessToken,
                Address = discoveryEndpoint.UserInfoEndpoint
            };

            var userInfo = await _httpClient.GetUserInfoAsync(userInfoRequest);

            if (userInfo.IsError)
            {
                throw new Exception($"User info alınamadı: {userInfo.Error}");
            }

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(userInfo.Claims, CookieAuthenticationDefaults.AuthenticationScheme, "name", "role");

            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            var authenticationProperties = new AuthenticationProperties();

            authenticationProperties.StoreTokens(new List<AuthenticationToken>
            {
                new AuthenticationToken
                {
                    Name=OpenIdConnectParameterNames.AccessToken,Value=token.AccessToken
                },
                new AuthenticationToken
                {
                    Name=OpenIdConnectParameterNames.RefreshToken,Value=token.RefreshToken
                },
                new AuthenticationToken
                {
                    Name=OpenIdConnectParameterNames.ExpiresIn,Value=DateTime.UtcNow.AddSeconds(token.ExpiresIn).ToString("o",CultureInfo.InvariantCulture)
                },
            });

            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authenticationProperties);

            return true;

        }
    }
}
