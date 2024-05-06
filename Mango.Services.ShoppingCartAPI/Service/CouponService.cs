using Mango.Services.ShoppingCartAPI.Models.Dto;
using Mango.Services.ShoppingCartAPI.Service.IService;
using Newtonsoft.Json;

namespace Mango.Services.ShoppingCartAPI.Service
{
    public class CouponService : ICouponService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CouponService(IHttpClientFactory httpClientFactory)
        {
             _httpClientFactory = httpClientFactory;
        }
        public async Task<CouponDto> GetCoupon(string couponCode)
        {
            // Create an HttpClient instance using the named client "Coupon" registered with IHttpClientFactory
            var client = _httpClientFactory.CreateClient("Coupon");

            // Send a GET request to the external API endpoint to retrieve coupon information by code
            var response = await client.GetAsync($"/api/coupon/GetByCode/{couponCode}");

            // Read the response content as a string
            var apiContent = await response.Content.ReadAsStringAsync();

            // Deserialize the response content into a ResponseDto object
            var resp = JsonConvert.DeserializeObject<ResponseDto>(apiContent);

            // Check if the API request was successful
            if (resp.IsSuccess)
            {
                // Deserialize the 'Result' property of the ResponseDto into a CouponDto object
                return JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(resp.Result));
            }

            // If the API request was not successful, return an empty CouponDto
            return new CouponDto();
        }
    }
}
