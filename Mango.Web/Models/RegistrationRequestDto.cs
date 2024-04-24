using System.ComponentModel.DataAnnotations;

namespace Mango.Web.Models
{
    // Represents a data transfer object (DTO) for registration requests
    public class RegistrationRequestDto
    {
        // Email of the user to be registered
        [Required]
        public string Email { get; set; }

        // Name of the user to be registered
        [Required]
        public string Name { get; set; }

        // Phone number of the user to be registered
        [Required]
        public string PhoneNumber { get; set; }

        // Password of the user to be registered
        [Required]
        public string Password { get; set; }

        // Role property of the user
        public string? Role { get; set; }
    }
}

