using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;

namespace Mango.Web.Service
{
    // Represents the implementation of the authentication service.
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;

        // Initializes a new instance of the AuthService class with the specified base service.
        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        // Asynchronously assigns a role to a user based on the provided registration details.
        public async Task<ResponseDto?> AssingRoleAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = registrationRequestDto,
                Url = SD.AuthAPIBase + "/api/auth/AssignRole"
            });
        }

        // Asynchronously performs user login based on the provided login request.
        public async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = loginRequestDto,
                Url = SD.AuthAPIBase + "/api/auth/login"
            });
        }

        // Asynchronously registers a new user based on the provided registration details.
        public async Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = registrationRequestDto,
                Url = SD.AuthAPIBase + "/api/auth/register"
            });
        }
    }
}
