using Mango.Web.Models;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Mango.Web.Controllers
{
    // Controller for handling views related to products
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        // Initializes a new instance of the ProductController class with an IProductService dependency
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // Action method for displaying the product index view
        public async Task<IActionResult> ProductIndex()
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

        // Action method for displaying the product creation form
        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }

        // Action method for handling product creation form submission
        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                // Call the service method to create a new product asynchronously
                ResponseDto? response = await _productService.CreateProductAsync(model);

                // Check if the product creation was successful
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Product created successfully!";

                    // Redirect to the product index view upon successful creation
                    return RedirectToAction(nameof(ProductIndex));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }

            // If model state is not valid or creation was unsuccessful, return to the create view with the model
            return View(model);
        }

        // Action method for deleting
        public async Task<IActionResult> ProductDelete(int productId)
        {
            // Call the service method to retrieve all products asynchronously
            ResponseDto? response = await _productService.GetProductByIdAsync(productId);

            // Deserialize the response and populate the list if the operation was successful
            if (response != null && response.IsSuccess)
            {
                ProductDto? model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));

                return View(model);
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return NotFound();
        }

        // Action method for handling product deletion
        [HttpPost]
        [ValidateAntiForgeryToken] // Ensures that the request token is valid
        public async Task<IActionResult> ProductDelete(ProductDto productDto)
        {
            // Call the service method to delete the product asynchronously
            ResponseDto? response = await _productService.DeleteProductAsync(productDto.ProductId);

            // Check if the product deletion was successful
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Product deleted successfully!";

                // Redirect to the product index view upon successful deletion
                return RedirectToAction(nameof(ProductIndex));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            // If deletion was unsuccessful, return to the view with the original product DTO
            return View(productDto);
        }

        // Action method for Editing
        public async Task<IActionResult> ProductEdit(int productId)
        {
            // Call the service method to retrieve all products asynchronously
            ResponseDto? response = await _productService.GetProductByIdAsync(productId);

            // Deserialize the response and populate the list if the operation was successful
            if (response != null && response.IsSuccess)
            {
                ProductDto? model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));

                return View(model);
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return NotFound();
        }

        // Action method for handling product editing
        [HttpPost]
        [ValidateAntiForgeryToken] // Ensures that the request token is valid
        public async Task<IActionResult> ProductEdit(ProductDto productDto)
        {
            // Call the service method to update the product asynchronously
            ResponseDto? response = await _productService.UpdateProductAsync(productDto);

            // Check if the product deletion was successful
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Product updated successfully!";

                // Redirect to the product index view upon successful editing
                return RedirectToAction(nameof(ProductIndex));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            // If editing was unsuccessful, return to the view with the original product DTO
            return View(productDto);
        }
    }
}
