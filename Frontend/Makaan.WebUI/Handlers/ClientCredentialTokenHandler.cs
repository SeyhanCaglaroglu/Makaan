using Makaan.WebUI.Services.ClientCredentialsTokenServices;
using System.Net.Http.Headers;

namespace Makaan.WebUI.Handlers
{
    public class ClientCredentialTokenHandler :DelegatingHandler
    {
        private readonly IClientCredentialsTokenService _clientCredentialsTokenService;

        public ClientCredentialTokenHandler(IClientCredentialsTokenService clientCredentialsTokenService)
        {
            _clientCredentialsTokenService = clientCredentialsTokenService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _clientCredentialsTokenService.GetToken();

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await base.SendAsync(request, cancellationToken);

            return response;
        }
    }
}
