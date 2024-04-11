using Mango.Web.Models;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mango.Web.Controllers
{
    // Controller for handling views related to coupons
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;

        // Initializes a new instance of the CouponController class with an ICouponService dependency
        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        // Action method for displaying the coupon index view
        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDto>? list = new();

            // Call the service method to retrieve all coupons asynchronously
            ResponseDto? response = await _couponService.GetAllCouponsAsync();

            // Deserialize the response and populate the list if the operation was successful
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
            }

            // Render the view with the list of coupons
            return View(list);
        }
    }
}
