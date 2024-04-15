using Mango.Web.Models;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;
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
            else
            {
                TempData["error"] = response?.Message;
            }

            // Render the view with the list of coupons
            return View(list);
        }

        // Action method for displaying the coupon creation form
        public async Task<IActionResult> CouponCreate()
        {
            return View();
        }

        // Action method for handling coupon creation form submission
        [HttpPost]
        public async Task<IActionResult> CouponCreate(CouponDto model)
        {
            if (ModelState.IsValid )
            {
                // Call the service method to create a new coupon asynchronously
                ResponseDto? response = await _couponService.CreateCouponAsync(model);

                // Check if the coupon creation was successful
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Coupon created successfully!";

                    // Redirect to the coupon index view upon successful creation
                    return RedirectToAction(nameof(CouponIndex));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }

            // If model state is not valid or creation was unsuccessful, return to the create view with the model
            return View(model);
        }

        // Action method for deleting
        public async Task<IActionResult> CouponDelete(int couponId)
        {
            // Call the service method to retrieve all coupons asynchronously
            ResponseDto? response = await _couponService.GetCouponByIdAsync(couponId);

            // Deserialize the response and populate the list if the operation was successful
            if (response != null && response.IsSuccess)
            {
                CouponDto? model = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(response.Result));
                
                return View(model);
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return NotFound();
        }

        // Action method for handling coupon deletion
        [HttpPost]
        [ValidateAntiForgeryToken] // Ensures that the request token is valid
        public async Task<IActionResult> CouponDelete(CouponDto couponDto)
        {
            // Call the service method to delete the coupon asynchronously
            ResponseDto? response = await _couponService.DeleteCouponAsync(couponDto.CouponId);

            // Check if the coupon deletion was successful
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Coupon deleted successfully!";

                // Redirect to the coupon index view upon successful deletion
                return RedirectToAction(nameof(CouponIndex));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            // If deletion was unsuccessful, return to the view with the original coupon DTO
            return View(couponDto);
        }
    }
}
