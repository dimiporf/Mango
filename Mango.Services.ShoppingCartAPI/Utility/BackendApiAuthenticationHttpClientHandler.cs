using Microsoft.AspNetCore.Authentication;  // Required for GetTokenAsync
using System.Net.Http.Headers;  // Required for AuthenticationHeaderValue

namespace Mango.Services.ShoppingCartAPI.Utility
{
    // Represents a custom HTTP client handler for authenticating outgoing requests with a bearer token.
    public class BackendApiAuthenticationHttpClientHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _accessor;

        // Initializes a new instance of the BackendApiAuthenticationHttpClientHandler class.
        public BackendApiAuthenticationHttpClientHandler(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        // Overrides the SendAsync method to attach a bearer token to outgoing HTTP requests.
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Retrieve the access token from the current HttpContext using the token name "access_token".
            var token = await _accessor.HttpContext.GetTokenAsync("access_token");

            // Attach the bearer token to the request's Authorization header.
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Continue sending the modified request through the HTTP pipeline.
            return await base.SendAsync(request, cancellationToken);
        }
    }
}

