using Mango.Web.Models;
using Mango.Web.Models;
using System.Threading.Tasks;

namespace Mango.Web.Service.IService
{
    // Defines an interface for interacting with product-related services.
    public interface IProductService
    {
        // Retrieves a product by its code asynchronously.
        Task<ResponseDto?> GetProductAsync(string productCode);

        // Retrieves all products asynchronously.
        Task<ResponseDto?> GetAllProductsAsync();

        // Retrieves a product by its ID asynchronously.
        Task<ResponseDto?> GetProductByIdAsync(int id);

        // Creates a new product asynchronously.
        Task<ResponseDto?> CreateProductAsync(ProductDto productDto);

        // Updates an existing product asynchronously.
        Task<ResponseDto?> UpdateProductAsync(ProductDto productDto);

        // Deletes a product by its ID asynchronously.
        Task<ResponseDto?> DeleteProductAsync(int id);
    }
}

