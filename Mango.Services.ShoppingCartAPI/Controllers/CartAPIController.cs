using AutoMapper;
using Mango.Services.ShoppingCartAPI.Data;
using Mango.Services.ShoppingCartAPI.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                var cartHeaderFromDb = await _db.CartHeaders.FirstOrDefaultAsync(u=>u.UserId == cartDto.CartHeader.UserId);
                if (cartHeaderFromDb == null)
                {
                    // create header and details
                }
                else
                {
                    // check for same products
                    var cartDetailsFromDb = await _db.CartDetails.FirstOrDefaultAsync(
                        u => u.ProductId == cartDto.CartDetails.First().ProductId &&
                        u.CartHeaderId == cartHeaderFromDb.CartHeaderId);
                    if (cartDetailsFromDb == null)
                    {
                        // create cartdetails
                    }
                    else
                    {
                        // update count in cart details
                    }
                }
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message.ToString();
                _response.IsSuccess = false;
            }
        }
    }
}
