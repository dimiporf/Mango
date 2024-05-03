using Mango.Web.Models;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Mango.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductDto>? list = new();

            // Call the service method to retrieve all products asynchronously
            ResponseDto? response = await _productService.GetAllProductsAsync();

            // Deserialize the response and populate the list if the operation was successful
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            // Render the view with the list of products
            return View(list);
        }

        // Retrieves and displays details of a product identified by the specified productId.
        [Authorize]
        public async Task<IActionResult> ProductDetails(int productId)
        {
            ProductDto model = new ProductDto(); // Initialize a new ProductDto object

            // Call the product service to retrieve product details by ID asynchronously
            ResponseDto response = await _productService.GetProductByIdAsync(productId);

            if (response != null && response.IsSuccess)
            {
                // Deserialize the product details from the response
                model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
            }
            else
            {
                // Set an error message in TempData if product retrieval fails
                TempData["error"] = response?.Message;
            }

            return View(model); // Return the view with the product details model
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
