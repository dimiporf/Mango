using System.ComponentModel.DataAnnotations;

namespace Mango.Services.CouponAPI.Models
{
    // Represents a coupon entity
    public class Coupon
    {
        // Gets or sets the ID of the coupon
        [Key]
        public int CouponId { get; set; }

        // Gets or sets the coupon code
        [Required]
        public string CouponCode { get; set; }

        // Gets or sets the discount amount of the coupon
        [Required]
        public double DiscountAmount { get; set; }

        // Gets or sets the minimum amount required to apply the coupon
        public int MinAmount { get; set; }
    }
}

