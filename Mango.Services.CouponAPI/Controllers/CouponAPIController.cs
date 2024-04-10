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

                else
                {
                    // Map the retrieved Coupon object to a CouponDto using AutoMapper
                    _response.Result = _mapper.Map<CouponDto>(obj);
                    _response.IsSuccess = true; // Indicate that the operation was successful
                }

            }
            catch (Exception ex)
            {
                // Handle exceptions that occur during database access or mapping
                _response.IsSuccess = false;
                _response.Message = $"An error occurred: {ex.Message}";
            }

            // Return the response object containing the result or error information
            return _response;
        }

        [HttpPost]
        public ResponseDto Post([FromBody] CouponDto couponDto)
        {
            try
            {
                // Map the incoming CouponDto to a Coupon entity using AutoMapper
                Coupon obj = _mapper.Map<Coupon>(couponDto);

                // Add the mapped Coupon entity to the database context
                _db.Coupons.Add(obj);

                // Save changes to persist the new coupon in the database
                _db.SaveChanges();

                // Map the saved Coupon entity back to a CouponDto and set it as the result
                _response.Result = _mapper.Map<CouponDto>(obj);

                // Indicate that the operation was successful
                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                // Handle exceptions that occur during database access or mapping
                _response.IsSuccess = false;
                _response.Message = $"An error occurred: {ex.Message}";
            }

            // Return the response object containing the result or error information
            return _response;
        }

        [HttpPut]
        public ResponseDto Put([FromBody] CouponDto couponDto)
        {
            try
            {
                // Map the incoming CouponDto to a Coupon entity using AutoMapper
                Coupon obj = _mapper.Map<Coupon>(couponDto);

                // Update the mapped Coupon entity in the database context
                _db.Coupons.Update(obj);

                // Save changes to persist the updated coupon in the database
                _db.SaveChanges();

                // Map the updated Coupon entity back to a CouponDto and set it as the result
                _response.Result = _mapper.Map<CouponDto>(obj);

                // Indicate that the operation was successful
                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                // Handle exceptions that occur during database access or mapping
                _response.IsSuccess = false;
                _response.Message = $"An error occurred: {ex.Message}";
            }

            // Return the response object containing the result or error information
            return _response;
        }


        [HttpDelete]
        public ResponseDto Delete(int id)
        {
            try
            {
                // Retrieve the Coupon entity with the specified ID from the database
                Coupon obj = _db.Coupons.FirstOrDefault(u => u.CouponId == id);

                if (obj == null)
                {
                    // If the specified coupon is not found, set IsSuccess to false
                    _response.IsSuccess = false;
                    _response.Message = $"Coupon with ID {id} not found.";
                }
                else
                {
                    // Remove the retrieved Coupon entity from the database context
                    _db.Coupons.Remove(obj);

                    // Save changes to persist the deletion
                    _db.SaveChanges();

                    // Indicate that the operation was successful
                    _response.IsSuccess = true;
                    _response.Message = $"Coupon with ID {id} has been deleted.";
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions that occur during database access or deletion
                _response.IsSuccess = false;
                _response.Message = $"An error occurred: {ex.Message}";
            }

            // Return the response object containing the operation outcome (success or failure)
            return _response;
        }


    }
}
