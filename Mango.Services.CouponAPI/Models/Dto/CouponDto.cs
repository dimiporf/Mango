namespace Mango.Services.CouponAPI.Models.Dto
{
    // Represents the data transfer object (DTO) for a coupon
    public class CouponDto
    {
        // Gets or sets the ID of the coupon
        public int CouponId { get; set; }

        // Gets or sets the coupon code
        public string CouponCode { get; set; }

        // Gets or sets the discount amount of the coupon
        public double DiscountAmount { get; set; }

        // Gets or sets the minimum amount required to apply the coupon
        public int MinAmount { get; set; }
    }
}

