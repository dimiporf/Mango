namespace Mango.Web.Models
{
    // Represents a data transfer object for the cart header.
    public class CartHeaderDto
    {
        // Cart header identifier.
        public int CartHeaderId { get; set; }

        // User identifier associated with the cart.
        public string? UserId { get; set; }

        // Coupon code applied to the cart.
        public string? CouponCode { get; set; }

        // Discount amount applied to the cart.
        public double Discount { get; set; }

        // Total amount of the cart after applying discounts.
        public double CartTotal { get; set; }
    }
}

