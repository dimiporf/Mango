using Mango.Services.ShoppingCartAPI.Models.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mango.Services.ShoppingCartAPI.Models
{
    // Represents the details of a product in a shopping cart.    
    public class CartDetails
    {
        [Key]
        public int CartDetailsId { get; set; } // Primary key for cart details

        public int CartHeaderId { get; set; } // Foreign key linking to the cart header

        [ForeignKey("CartHeaderId")]
        public CartHeader CartHeader { get; set; } // Navigation property to the associated cart header

        public int ProductId { get; set; } // ID of the product in the cart

        [NotMapped]
        public ProductDto Product { get; set; } // Not mapped to database; holds the product details

        public int Count { get; set; } // Quantity of the product in the cart
    }
}

