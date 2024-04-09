using AutoMapper;
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
        private IMapper _mapper;

        // Initializes a new instance of the CouponAPIController class
        public CouponAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _response = new ResponseDto();
            _mapper = mapper;
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
                _response.Result = _mapper.Map<IEnumerable<CouponDto>>(objList);

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

                // Map the retrieved Coupon object to CouponDto using IMapper instance
                _response.Result = _mapper.Map<CouponDto>(obj);


            }
            catch (Exception ex)
            {
                // Handle exceptions here
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        
        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDto GetByCode(string code)
        {
            try
            {
                // Attempt to retrieve a coupon by its code from the database (case-insensitive comparison)
                Coupon obj = _db.Coupons.FirstOrDefault(u => u.CouponCode.ToLower() == code.ToLower());

                if (obj == null)
                {
                    // If the coupon is not found, set IsSuccess to false and provide a relevant message
                    _response.IsSuccess = false;
                    _response.Message = "Coupon not found.";
                }

                // Map the retrieved Coupon object to CouponDto using IMapper instance
                _response.Result = _mapper.Map<CouponDto>(obj);


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
