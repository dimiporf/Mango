using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mango.Services.ShoppingCartAPI.Models
{
    // Represents the header information for a shopping cart.
    public class CartHeader
    {
        [Key]
        public int CartHeaderId { get; set; } // Primary key for the cart header

        public string? UserId { get; set; } // ID of the user associated with the cart

        public string? CouponCode { get; set; } // Coupon code applied to the cart

        [NotMapped]
        public double Discount { get; set; } // Not mapped to database; holds the discount amount

        [NotMapped]
        public double CartTotal { get; set; } // Not mapped to database; holds the total cart value
    }
}
