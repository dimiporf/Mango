using Mango.Web.Models;
using Mango.Web.Service.IService;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static Mango.Web.Utility.SD;

namespace Mango.Web.Service
{
    // Represents a base service implementation for sending HTTP requests.
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly ITokenProvider _tokenProvider;

        // Initializes a new instance of the BaseService class.
        public BaseService(IHttpClientFactory httpClientFactory, ITokenProvider tokenProvider)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _tokenProvider = tokenProvider;
        }

        // Sends an asynchronous HTTP request based on the provided RequestDto and returns a ResponseDto.
        public async Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true)
        {
            try
            {
                // Create an HTTP client using the named client ("MangoAPI") from the factory.
                HttpClient client = _httpClientFactory.CreateClient("MangoAPI");

                // Create a new HTTP request message.
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");

                // Check if the 'withBearer' flag is true for token authorization
                if (withBearer)
                {
                    var token = _tokenProvider.GetToken(); // Retrieve the JWT token from the token provider

                    if (!string.IsNullOrEmpty(token))
                    {
                        // If a valid token is retrieved
                        message.Headers.Add("Authorization", $"Bearer {token}");
                        // Add the Bearer token to the 'Authorization' header of the HTTP request
                    }
                }


                // Set the request URL based on the RequestDto's Url property.
                message.RequestUri = new Uri(requestDto.Url);

                // Serialize the request data (if any) to JSON and set it as the message content.
                if (requestDto.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
                }

                HttpResponseMessage? apiResponse = null;

                // Set the HTTP method based on the ApiType specified in the RequestDto.
                switch (requestDto.ApiType)
                {
                    case ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                // Send the HTTP request asynchronously and await the response.
                apiResponse = await client.SendAsync(message);

                // Handle different HTTP status codes in the response.
                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new ResponseDto { IsSuccess = false, Message = "Not Found" };
                    case HttpStatusCode.Forbidden:
                        return new ResponseDto { IsSuccess = false, Message = "Access Denied" };
                    case HttpStatusCode.Unauthorized:
                        return new ResponseDto { IsSuccess = false, Message = "Unauthorized" };
                    case HttpStatusCode.InternalServerError:
                        return new ResponseDto { IsSuccess = false, Message = "Internal Server Error" };
                    default:
                        // Deserialize the response content to a ResponseDto object.
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                        return apiResponseDto;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions by returning a ResponseDto with error details.
                var dto = new ResponseDto
                {
                    Message = ex.Message.ToString(),
                    IsSuccess = false
                };
                return dto;
            }
        }
    }
}
