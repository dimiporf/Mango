﻿using Mango.Web.Models;
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

        public async Task<IActionResult> Remove(int cartDetailsId)
        {
            // Retrieve the user ID from the JWT claims
            var userId = User.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub)?.Value;

            // Call the CartService to remove the item from the cart asynchronously
            ResponseDto? response = await _cartService.RemoveFromCartAsync(cartDetailsId);

            // Check if the item removal was successful
            if (response != null && response.IsSuccess)
            {
                // Set a success message in TempData and redirect to the CartIndex action
                TempData["success"] = "Cart updated successfully!";
                return RedirectToAction(nameof(CartIndex));
            }

            // If the removal operation fails or encounters an error, return an empty view
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ApplyCoupon(CartDto cartDto)
        {            
            ResponseDto? response = await _cartService.ApplyCouponAsync(cartDto);
                        
            if (response != null & response.IsSuccess)
            {               
                TempData["success"] = "Cart updated successfully!";
                return RedirectToAction(nameof(CartIndex));
            }
            
            return View();
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

