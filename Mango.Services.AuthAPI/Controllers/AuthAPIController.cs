using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Mango.Services.AuthAPI.Controllers
{
    // Controller for handling authentication-related API endpoints
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        private ResponseDto _response;

        public AuthAPIController(IAuthService authService)
        {
            _authService = authService;
            _response = new();
        }


        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
        {
            var errorMessage = await _authService.Register(model);
            if(!string.IsNullOrEmpty(errorMessage))
            {
                _response.IsSuccess = false;
                _response.Message = errorMessage;
                return BadRequest(_response);
            }

            // Return an Ok response indicating successful registration
            return Ok(_response);
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            // Call the AuthService to perform user login
            var loginResponse = await _authService.Login(model);

            // Check if the loginResponse contains a valid user
            if (loginResponse.User == null)
            {
                // If the user is not found or authentication fails, return a BadRequest with an error message
                _response.IsSuccess = false;
                _response.Message = "Username or Password are incorrect";
                return BadRequest(_response);
            }

            // Set the result in the response to the loginResponse
            _response.Result = loginResponse;

            // Return an Ok response indicating successful login along with the loginResponse
            return Ok(_response);
        }

    }
}

