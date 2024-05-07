using Mango.Web.Models;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Mango.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // Action method to display the cart for the authenticated user
        [Authorize]
        public async Task<IActionResult> CartIndex()
        {
            // Load the cart details based on the logged-in user
            return View(await LoadCartDtoBasedOnLoggedUser());
        }

        // Helper method to load the cart details based on the logged-in user
        private async Task<CartDto> LoadCartDtoBasedOnLoggedUser()
        {
            // Retrieve the user ID from the JWT claims
            var userId = User.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub)?.Value;

            // Call the CartService to retrieve the cart for the specified user ID
            ResponseDto? response = await _cartService.GetCartByUserIdAsync(userId);

            // Check if the cart retrieval was successful
            if (response != null && response.IsSuccess)
            {
                // Deserialize the response into a CartDto object
                CartDto cartDto = JsonConvert.DeserializeObject<CartDto>(Convert.ToString(response.Result));
                return cartDto;
            }

            // Return a new CartDto if the cart retrieval fails
            return new CartDto();
        }
    }
}

