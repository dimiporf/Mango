using System.ComponentModel.DataAnnotations;

namespace Mango.Web.Models
{
    // Represents a data transfer object (DTO) for login requests
    public class LoginRequestDto
    {
        // Username (typically an email address) used for login
        [Required]
        public string UserName { get; set; }

        // Password associated with the username for login
        [Required]
        public string Password { get; set; }
    }
}

