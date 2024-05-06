using AutoMapper;
using Mango.Services.ShoppingCartAPI.Data;
using Mango.Services.ShoppingCartAPI.Models;
using Mango.Services.ShoppingCartAPI.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;

namespace Mango.Services.ShoppingCartAPI.Controllers
{
    // Represents the controller for managing shopping cart operations via API endpoints.
    [Route("api/cart")]
    [ApiController]
    public class CartAPIController : ControllerBase
    {
        private ResponseDto _response; // Holds the response data for API operations.
        private IMapper _mapper; // Handles object mapping operations.
        private readonly AppDbContext _db; // Represents the application database context.

        // Initializes a new instance of the CartAPIController class.
        public CartAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            this._response = new ResponseDto(); // Initialize the response object.
            _mapper = mapper; // Assign the mapper for object mapping operations.
        }

        // Handles the HTTP POST request to upsert a shopping cart.
        [HttpPost("CartUpsert")]
        public async Task<ResponseDto> CartUpsert(CartDto cartDto)
        {
            try
            {
                // Retrieve the cart header from the database based on the user ID.
                var cartHeaderFromDb = await _db.CartHeaders.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == cartDto.CartHeader.UserId);

                if (cartHeaderFromDb == null)
                {
                    // If the cart header doesn't exist, create a new one based on the DTO.
                    CartHeader cartHeader = _mapper.Map<CartHeader>(cartDto.CartHeader);
                    _db.CartHeaders.Add(cartHeader);
                    await _db.SaveChangesAsync();

                    // Set the CartHeaderId for the first CartDetails item and add it to the database.
                    cartDto.CartDetails.First().CartHeaderId = cartHeader.CartHeaderId;
                    _db.CartDetails.Add(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                    await _db.SaveChangesAsync();
                }
                else
                {
                    // Check if the product already exists in the cart details for the given cart header.
                    var cartDetailsFromDb = await _db.CartDetails.AsNoTracking().FirstOrDefaultAsync(
                        u => u.ProductId == cartDto.CartDetails.First().ProductId &&
                             u.CartHeaderId == cartHeaderFromDb.CartHeaderId);

                    if (cartDetailsFromDb == null)
                    {
                        // Add new cart details to the existing cart header.
                        cartDto.CartDetails.First().CartHeaderId = cartHeaderFromDb.CartHeaderId;
                        _db.CartDetails.Add(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                        await _db.SaveChangesAsync();
                    }
                    else
                    {
                        // Update existing cart details by combining counts and IDs.
                        cartDto.CartDetails.First().Count += cartDetailsFromDb.Count;
                        cartDto.CartDetails.First().CartHeaderId = cartDetailsFromDb.CartHeaderId;
                        cartDto.CartDetails.First().CartDetailsId = cartDetailsFromDb.CartDetailsId;

                        _db.CartDetails.Update(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                        await _db.SaveChangesAsync();
                    }
                }

                _response.Result = cartDto; // Set the result to the updated CartDto.
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the cart upsert operation.
                _response.Message = ex.Message.ToString();
                _response.IsSuccess = false;
            }

            return _response; // Return the response object after processing the cart upsert operation.
        }


        // Handles the HTTP POST request to remove a cart item from the shopping cart.
        [HttpPost("RemoveCart")]
        public async Task<ResponseDto> RemoveCart([FromBody] int cardDetailsId)
        {
            try
            {
                // Retrieve the cart details by ID from the database.
                CartDetails cartDetails = _db.CartDetails.First(u => u.CartDetailsId == cardDetailsId);

                // Count the total number of cart items associated with the same cart header.
                int totalCountOfCartItems = _db.CartDetails.Where(u => u.CartHeaderId == cartDetails.CartHeaderId).Count();

                // Remove the specified cart details from the database.
                _db.CartDetails.Remove(cartDetails);

                // If the total count of cart items for the cart header is 1, remove the cart header as well.
                if (totalCountOfCartItems == 1)
                {
                    var cartHeaderToRemove = await _db.CartHeaders.FirstOrDefaultAsync(u => u.CartHeaderId == cartDetails.CartHeaderId);
                    _db.CartHeaders.Remove(cartHeaderToRemove);
                }

                // Save changes to the database.
                await _db.SaveChangesAsync();

                _response.Result = true; // Set the result to true indicating successful removal.
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the cart item removal process.
                _response.Message = ex.Message.ToString();
                _response.IsSuccess = false;
            }

            return _response; // Return the response object after processing the cart item removal.
        }

    }
}
