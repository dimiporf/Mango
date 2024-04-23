using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    // Controller for handling authentication-related actions.
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;

        // Initializes a new instance of the AuthController class with the specified authentication service.
        public AuthController(IAuthService authService, ITokenProvider tokenProvider)
        {
            _authService = authService;
            _tokenProvider = tokenProvider;
        }

        // GET: /Auth/Login
        [HttpGet]
        public IActionResult Login()
        {
            // Prepare a new login request DTO and return the corresponding view
            LoginRequestDto loginRequestDto = new();
            return View(loginRequestDto);
        }

        // POST: /Auth/Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto obj)
        {
            // Call the authentication service to perform user login asynchronously
            ResponseDto responseDto = await _authService.LoginAsync(obj);

            if (responseDto != null && responseDto.IsSuccess)
            {
                // Deserialize the login response and redirect to the home page upon successful login
                LoginResponseDto loginResponseDto = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(responseDto.Result));

                // Set the retrieved token in the token provider for subsequent requests
                _tokenProvider.SetToken(loginResponseDto.Token);

                // Redirect to the home page upon successful login
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Display a custom error message in case of failed login
                ModelState.AddModelError("CustomError", responseDto.Message);
                return View(obj);
            }
        }


        // GET: /Auth/Register
        [HttpGet]
        public IActionResult Register()
        {
            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{Text=SD.RoleAdmin, Value=SD.RoleAdmin},
                new SelectListItem{Text=SD.RoleCustomer, Value=SD.RoleCustomer}
            };

            ViewBag.RoleList = roleList;

            // Return the registration view
            return View();
        }

        // POST: /Auth/Register
        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDto obj)
        {
            // Call the authentication service to register the user asynchronously
            ResponseDto result = await _authService.RegisterAsync(obj);
            ResponseDto assignRole;

            if (result != null && result.IsSuccess)
            {
                // Assign a default role if no role was provided
                if (string.IsNullOrEmpty(obj.Role))
                {
                    obj.Role = SD.RoleCustomer;
                }

                // Call the authentication service to assign a role to the registered user asynchronously
                assignRole = await _authService.AssingRoleAsync(obj);

                if (assignRole != null && assignRole.IsSuccess)
                {
                    // Set a success message and redirect to the login page upon successful registration and role assignment
                    TempData["success"] = "Registration Successful";
                    return RedirectToAction(nameof(Login));
                }
            }

            // Prepare a list of role options to be displayed in the view
            var roleList = new List<SelectListItem>()
            {
                   new SelectListItem { Text = SD.RoleAdmin, Value = SD.RoleAdmin },
                   new SelectListItem { Text = SD.RoleCustomer, Value = SD.RoleCustomer }
            };

            // Pass the role list to the view via ViewBag
            ViewBag.RoleList = roleList;

            // Return the registration view with the provided model (obj) if registration or role assignment fails
            return View(obj);
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
