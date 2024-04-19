using Mango.Services.AuthAPI.Models.Dto; // Importing the necessary DTOs

namespace Mango.Services.AuthAPI.Service.IService
{
    // Interface for authentication service
    public interface IAuthService
    {
        // Method to handle user registration
        Task<string> Register(RegistrationRequestDto registrationRequestDto);

        // Method to handle user login
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);

        // Method to assign role to the user
        Task<bool> AssignRole(string email, string roleName);
    }
}
