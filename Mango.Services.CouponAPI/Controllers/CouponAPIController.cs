using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
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

        // Initializes a new instance of the CouponAPIController class
        public CouponAPIController(AppDbContext db)
        {
            _db = db;
        }

        // GET: api/couponapi
        [HttpGet]
        // Retrieves all coupons from the database
        public object Get()
        {
            try
            {
                // Retrieves all coupons from the database
                IEnumerable<Coupon> objList = _db.Coupons.ToList();
                return objList;
            }
            catch (Exception ex)
            {
                // Handle exceptions here
            }
            return null;
        }
    }
}
