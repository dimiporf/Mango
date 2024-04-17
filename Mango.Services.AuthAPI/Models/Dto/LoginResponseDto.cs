namespace Mango.Services.AuthAPI.Models.Dto
{
    // Represents a data transfer object (DTO) for login responses
    public class LoginResponseDto
    {
        // User information associated with the login
        public UserDto User { get; set; }

        // Authentication token generated upon successful login
        public string Token { get; set; }
    }
}

