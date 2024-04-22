using Mango.Web.Models;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Web.Controllers
{
    // Controller for handling authentication-related actions.
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        // Initializes a new instance of the AuthController class with the specified authentication service.
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // GET: /Auth/Login
        [HttpGet]
        public IActionResult Login()
        {
            // Prepare a new login request DTO and return the corresponding view
            LoginRequestDto loginRequestDto = new();
            return View(loginRequestDto);
        }

        // GET: /Auth/Register
        [HttpGet]
        public IActionResult Register()
        {
            // Return the registration view
            return View();
        }

        // GET: /Auth/Logout
        [HttpGet]
        public IActionResult Logout()
        {
            // Return the logout view
            return View();
        }
    }
}
