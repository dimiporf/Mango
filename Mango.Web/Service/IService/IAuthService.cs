using Mango.Web.Models;

namespace Mango.Web.Service.IService
{
    // Represents the service contract for authentication operations.
    public interface IAuthService
    {
        // Asynchronously performs user login based on the provided credentials.
        Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto);

        // Asynchronously registers a new user based on the provided registration details.
        Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto);

        // Asynchronously assigns a role to a user based on the registration request details.
        Task<ResponseDto?> AssingRoleAsync(RegistrationRequestDto registrationRequestDto);
    }
}

