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
        private readonly IJwtTokenGenerator _jwtTokenGenerator; // You know what is this

        // Constructor to initialize AuthService with required dependencies
        public AuthService(AppDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        // Asynchronously assigns the specified role to a user identified by their email.
        public async Task<bool> AssignRole(string email, string roleName)
        {
            // Find the user by email (case-insensitive)
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());

            if (user != null)
            {
                // Check if the role exists
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    // Create role if it does not already exist
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                // Add the user to the specified role
                await _userManager.AddToRoleAsync(user, roleName);

                return true; // Role assigned successfully
            }
            return false; // User not found or role assignment failed
        }

        // Method to handle user login and generate JWT token upon successful login
        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            // Find the user by username (case-insensitive)
            var user = _db.ApplicationUsers.FirstOrDefault(u=>u.UserName.ToLower()==loginRequestDto.UserName.ToLower());

            // Check if the user exists and the provided password is valid
            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if (user==null  || isValid== false)
            {
                // If user is not found or password is invalid, return an empty LoginResponseDto
                return new LoginResponseDto()
                {
                    User = null,
                    Token = ""
                };
            }

            var roles = await _userManager.GetRolesAsync(user);

            var token = _jwtTokenGenerator.GenerateToken(user, roles);

            // Create a UserDto object from the authenticated user

            UserDto userDto = new()
            {
                Email = user.Email,
                ID = user.Id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber
            };

            // Construct a LoginResponseDto with the authenticated user and an empty token
            LoginResponseDto loginResponseDto = new LoginResponseDto
            {
                User = userDto,
                Token = token // Generate and assign the JWT token here
            };

            // Return the LoginResponseDto containing the authenticated user and JWT token
            return loginResponseDto;
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
