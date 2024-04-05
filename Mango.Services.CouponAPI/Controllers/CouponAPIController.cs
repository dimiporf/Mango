using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mango.Services.CouponAPI.Controllers
{
    // Controller for handling coupon-related API endpoints
    [Route("api/[controller]")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;

        // Initializes a new instance of the CouponAPIController class
        public CouponAPIController(AppDbContext db)
        {
            _db = db;
            _response = new ResponseDto();
        }

        // GET: api/couponapi
        [HttpGet]
        // Retrieves all coupons from the database
        public ResponseDto GetAllCoupons()
        {
            try
            {
                // Retrieves all coupons from the database
                IEnumerable<Coupon> objList = _db.Coupons.ToList();
                _response.Result = objList;
                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        // GET: api/couponapi/{id}
        [HttpGet("{id:int}")]
        // Retrieves a coupon by its ID from the database
        public ResponseDto GetCouponById(int id)
        {
            try
            {
                // Retrieves a coupon by its ID from the database
                Coupon obj = _db.Coupons.First(u => u.CouponId == id);
                _response.Result = obj;
                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
