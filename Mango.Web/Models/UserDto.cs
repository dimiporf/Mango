namespace Mango.Web.Models
{
    // Represents a data transfer object (DTO) for user-related information
    public class UserDto
    {
        // Unique identifier for the user
        public string ID { get; set; }

        // Name of the user
        public string Name { get; set; }

        // Email address of the user
        public string Email { get; set; }

        // Phone number of the user
        public string PhoneNumber { get; set; }
    }
}

