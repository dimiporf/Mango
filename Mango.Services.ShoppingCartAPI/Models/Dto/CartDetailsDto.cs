namespace Mango.Services.ShoppingCartAPI.Models.Dto
{
    // Represents a data transfer object for cart details.
    public class CartDetailsDto
    {
        // Cart details identifier.
        public int CartDetailsId { get; set; }

        // Identifier of the associated cart header.
        public int CartHeaderId { get; set; }

        // Reference to the cart header associated with these details.
        public CartHeaderDto? CartHeader { get; set; }

        // Identifier of the product in the cart.
        public int ProductId { get; set; }

        // Product information associated with the cart details.
        public ProductDto? Product { get; set; }

        // Count of the product units in the cart.
        public int Count { get; set; }
    }
}

