using Mango.Web.Models;
using System.Threading.Tasks;
namespace Mango.Web.Service.IService
{
    public interface ICartService
    {
        // Method to retrieve a cart by user ID asynchronously
        Task<ResponseDto?> GetCartByUserIdAsync(string userId);

        // Method to upsert (create/update) a cart asynchronously
        Task<ResponseDto?> UpsertCartAsync(CartDto cartDto);

        // Method to remove an item from the cart by cart details ID asynchronously
        Task<ResponseDto?> RemoveFromCartAsync(int cartDetailsId);

        // Method to apply a coupon to the cart asynchronously
        Task<ResponseDto?> ApplyCouponAsync(CartDto cartDto);
    }
}


