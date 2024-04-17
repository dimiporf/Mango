using Mango.Services.AuthAPI.Data; // Importing the necessary data context
using Mango.Services.AuthAPI.Models; // Importing the necessary models
using Mango.Services.AuthAPI.Models.Dto; // Importing the necessary DTOs
using Mango.Services.AuthAPI.Service.IService; // Importing the necessary service interface
using Microsoft.AspNetCore.Identity; // Importing Identity framework for user management

namespace Mango.Services.AuthAPI.Service
{
    // Service class responsible for handling authentication operations
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db; // Database context for authentication data
        private readonly UserManager<ApplicationUser> _userManager; // User manager for identity operations
        private readonly RoleManager<IdentityRole> _roleManager; // Role manager for identity role operations

        // Constructor to initialize AuthService with required dependencies
        public AuthService(AppDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // Method to handle user login
        public Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            throw new NotImplementedException(); // Placeholder implementation
        }

        // Method to handle user registration
        public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
        {
            // Create a new ApplicationUser instance with the provided registration details
            ApplicationUser user = new()
            {
                UserName = registrationRequestDto.Email,
                Email = registrationRequestDto.Email,
                NormalizedEmail = registrationRequestDto.Email.ToUpper(),
                Name = registrationRequestDto.Name,
                PhoneNumber = registrationRequestDto.PhoneNumber
            };
            try
            {
                // Attempt to create the user in the database using UserManager
                var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
                if (result.Succeeded)
                {
                    // If user creation is successful, retrieve the created user from the database
                    var userToReturn = _db.ApplicationUsers.First(u=>u.UserName == registrationRequestDto.Email);

                    // Map the retrieved ApplicationUser to a UserDto
                    UserDto userDto = new()
                    {
                        Email = userToReturn.Email,
                        ID = userToReturn.Id,
                        Name = userToReturn.Name,
                        PhoneNumber = userToReturn.PhoneNumber
                    };

                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {

            }
            return "Error Encountered";
        }
    }
}
