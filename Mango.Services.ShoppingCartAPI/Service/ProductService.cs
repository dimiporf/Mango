using Mango.Services.ShoppingCartAPI.Models.Dto;
using Mango.Services.ShoppingCartAPI.Service.IService;
using Newtonsoft.Json;

namespace Mango.Services.ShoppingCartAPI.Service
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Retrieves a collection of products asynchronously from the product API.
        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            // Create an HTTP client named "Product" using the injected HTTP client factory.
            var client = _httpClientFactory.CreateClient("Product");

            // Send an HTTP GET request to the product API endpoint.
            var response = await client.GetAsync("/api/product");

            // Read the response content as a string.
            var apiContent = await response.Content.ReadAsStringAsync();

            // Deserialize the API response into a ResponseDto object.
            var resp = JsonConvert.DeserializeObject<ResponseDto>(apiContent);

            // Check if the API response indicates success.
            if (resp.IsSuccess)
            {
                // Deserialize the result field into a collection of ProductDto objects.
                return JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(Convert.ToString(resp.Result));
            }

            // If the API response is not successful, return an empty list of ProductDto.
            return new List<ProductDto>();
        }
    }
}
