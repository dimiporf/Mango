using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;
using System.Threading.Tasks;

namespace Mango.Web.Service
{
    // Service implementation for interacting with the ProductAPI
    public class ProductService : IProductService
    {
        private readonly IBaseService _baseService;

        // Initializes a new instance of the ProductService class with an IBaseService dependency
        public ProductService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        // Sends a request to create a new coupon via the ProductAPI
        public async Task<ResponseDto?> CreateProductAsync(ProductDto productDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = productDto,
                Url = SD.ProductAPIBase + "/api/product"
            });
        }

        // Sends a request to delete a coupon by ID via the ProductAPI
        public async Task<ResponseDto?> DeleteProductAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ProductAPIBase + "/api/product/" + id
            });
        }

        // Sends a request to retrieve all coupons via the ProductAPI
        public async Task<ResponseDto?> GetAllProductsAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "/api/product"
            });
        }

        // Sends a request to retrieve a coupon by its code via the ProductAPI
        public async Task<ResponseDto?> GetProductAsync(string productCode)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "/api/product/GetByCode/" + productCode
            });
        }

        // Sends a request to retrieve a coupon by ID via the ProductAPI
        public async Task<ResponseDto?> GetProductByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "/api/product/" + id
            });
        }

        // Sends a request to update a coupon via the ProductAPI
        public async Task<ResponseDto?> UpdateProductAsync(ProductDto productDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.PUT,
                Data = productDto,
                Url = SD.ProductAPIBase + "/api/product"
            });
        }
    }
}
