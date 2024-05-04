namespace Mango.Services.ShoppingCartAPI.Models.Dto
{
    // Represents a data transfer object for a complete shopping cart.
    public class CartDto
    {
        // Represents the cart header information.
        public CartHeaderDto CartHeader { get; set; }

        // Represents the collection of cart details associated with the cart.
        public IEnumerable<CartDetailsDto> CartDetails { get; set; }
    }
}

