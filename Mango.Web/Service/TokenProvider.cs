using Mango.Web.Service.IService;
using Mango.Web.Utility;
using Newtonsoft.Json.Linq;

namespace Mango.Web.Service
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public TokenProvider(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        // Clears the stored token from the HTTP context cookies.
        public void ClearToken()
        {
            _contextAccessor.HttpContext?.Response.Cookies.Delete(SD.TokenCookie);
        }

        // Retrieves the stored token from the HTTP context cookies.
        public string? GetToken()
        {
            string? token = null;

            // Attempt to retrieve the token from cookies.
            bool? hasToken = _contextAccessor.HttpContext?.Request.Cookies.TryGetValue(SD.TokenCookie, out token);

            // Return the token if it exists; otherwise, return null.
            return hasToken is true ? token : null;
        }

        // Sets a token in the HTTP context cookies.
        public void SetToken(string token)
        {
            // Append the token to cookies in the HTTP response.
            _contextAccessor.HttpContext?.Response.Cookies.Append(SD.TokenCookie, token);
        }
    }
}

