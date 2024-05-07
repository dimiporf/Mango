using IdentityModel;
using Mango.Web.Models;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;

namespace Mango.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public HomeController(IProductService productService, ICartService cartService)
        {
            _productService = productService;
            _cartService = cartService;
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

        [Authorize]
        [HttpPost]
        [ActionName("ProductDetails")]
        public async Task<IActionResult> ProductDetails(ProductDto productDto)
        {
            // Retrieve the user ID from the JWT claims
            string userId = User.Claims.FirstOrDefault(u => u.Type == JwtClaimTypes.Subject)?.Value;

            // Create a new CartDto object representing the user's cart
            CartDto cartDto = new CartDto()
            {
                CartHeader = new CartHeaderDto
                {
                    UserId = userId
                }
            };

            // Create a CartDetailsDto object representing the product to be added to the cart
            CartDetailsDto cartDetails = new CartDetailsDto()
            {
                Count = productDto.Count,
                ProductId = productDto.ProductId,
            };

            // Add the CartDetailsDto object to a list of cart details
            List<CartDetailsDto> cartDetailsDtos = new() { cartDetails };

            // Assign the list of cart details to the CartDto
            cartDto.CartDetails = cartDetailsDtos;

            // Call the CartService to upsert (create/update) the cart asynchronously
            ResponseDto? response = await _cartService.UpsertCartAsync(cartDto);

            // Check if the cart upsert operation was successful
            if (response != null && response.IsSuccess)
            {
                // Set a success message in TempData and redirect to the Index action
                TempData["success"] = "Item has been added to the Shopping Cart";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Set an error message in TempData if cart upsert fails
                TempData["error"] = response?.Message;
            }

            // Return the ProductDto to the view (typically used for error handling)
            return View(productDto);
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
