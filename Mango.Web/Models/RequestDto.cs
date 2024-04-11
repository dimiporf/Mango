using System.Security.AccessControl;
using static Mango.Web.Utility.SD;

namespace Mango.Web.Models
{
    // Represents a request data transfer object (DTO) used for making HTTP requests.
    public class RequestDto
    {
        // Gets or sets the HTTP method type (default: "GET")
        public ApiType ApiType { get; set; } = ApiType.GET;

        // Gets or sets the URL to send the HTTP request
        public string Url { get; set; }

        // Gets or sets the optional data payload for the HTTP request
        public object Data { get; set; }

        // Gets or sets the access token for authentication purposes
        public string AccessToken { get; set; }
    }
}
