namespace Mango.Services.AuthAPI.Models.Dto
{
    // Represents a data transfer object (DTO) for registration requests
    public class RegistrationRequestDto
    {
        // Email of the user to be registered
        public string Email { get; set; }

        // Name of the user to be registered
        public string Name { get; set; }

        // Phone number of the user to be registered
        public string PhoneNumber { get; set; }

        // Password of the user to be registered
        public string Password { get; set; }

        // Role property of the user
        public string? Role { get; set; }
    }
}

