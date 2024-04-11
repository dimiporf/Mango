using Mango.Web.Models;
using System.Threading.Tasks;

namespace Mango.Web.Service.IService
{
    // Defines an interface for interacting with coupon-related services.
    public interface ICouponService
    {
        // Retrieves a coupon by its code asynchronously.
        Task<ResponseDto?> GetCouponAsync(string couponCode);

        // Retrieves all coupons asynchronously.
        Task<ResponseDto?> GetAllCouponsAsync();

        // Retrieves a coupon by its ID asynchronously.
        Task<ResponseDto?> GetCouponByIdAsync(int id);

        // Creates a new coupon asynchronously.
        Task<ResponseDto?> CreateCouponAsync(CouponDto couponDto);

        // Updates an existing coupon asynchronously.
        Task<ResponseDto?> UpdateCouponAsync(CouponDto couponDto);

        // Deletes a coupon by its ID asynchronously.
        Task<ResponseDto?> DeleteCouponAsync(int id);
    }
}

