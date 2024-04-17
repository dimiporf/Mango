namespace Mango.Services.AuthAPI.Models.Dto
{
    // Represents a data transfer object (DTO) for login requests
    public class LoginRequestDto
    {
        // Username (typically an email address) used for login
        public string UserName { get; set; }

        // Password associated with the username for login
        public string Password { get; set; }
    }
}

