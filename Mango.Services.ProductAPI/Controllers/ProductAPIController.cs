using AutoMapper;
using Mango.Services.ProductAPI.Data;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mango.Services.ProductAPI.Controllers
{
    // Controller for handling Product-related API endpoints
    [Route("api/product")]
    [ApiController]
    //[Authorize]
    public class ProductAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;

        // Initializes a new instance of the ProductAPIController class
        public ProductAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _response = new ResponseDto();
            _mapper = mapper;
        }

        // GET: api/Productapi
        [HttpGet]
        // Retrieves all Products from the database
        public ResponseDto GetAllProducts()
        {
            try
            {
                // Retrieves all Products from the database
                IEnumerable<Product> objList = _db.Products.ToList();
                _response.Result = _mapper.Map<IEnumerable<ProductDto>>(objList);

            }
            catch (Exception ex)
            {
                // Handle exceptions here
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        // GET: api/Productapi/{id}
        [HttpGet("{id:int}")]
        // Retrieves a Product by its ID from the database
        public ResponseDto GetProductById(int id)
        {
            try
            {
                // Retrieves a Product by its ID from the database
                Product obj = _db.Products.First(u => u.ProductId == id);

                // Map the retrieved Product object to ProductDto using IMapper instance
                _response.Result = _mapper.Map<ProductDto>(obj);


            }
            catch (Exception ex)
            {
                // Handle exceptions here
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
         

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Post([FromBody] ProductDto ProductDto)
        {
            try
            {
                // Map the incoming ProductDto to a Product entity using AutoMapper
                Product obj = _mapper.Map<Product>(ProductDto);

                // Add the mapped Product entity to the database context
                _db.Products.Add(obj);

                // Save changes to persist the new Product in the database
                _db.SaveChanges();

                // Map the saved Product entity back to a ProductDto and set it as the result
                _response.Result = _mapper.Map<ProductDto>(obj);

                // Indicate that the operation was successful
                _response.IsSuccess = true;
                _response.Message = $"The Product with ID {obj.ProductId} has been created.";
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
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Put([FromBody] ProductDto ProductDto)
        {
            try
            {
                // Map the received ProductDto to a Product entity
                Product obj = _mapper.Map<Product>(ProductDto);

                // Update the existing Product entity in the database
                _db.Products.Update(obj);

                // Save changes to persist the update
                _db.SaveChanges();

                // Map the updated Product entity back to a ProductDto
                _response.Result = _mapper.Map<ProductDto>(obj);

                // Set a success message indicating the update was successful
                _response.IsSuccess = true;
                _response.Message = $"Product with ID {obj.ProductId} updated successfully.";
            }
            catch (Exception ex)
            {
                // Handle exceptions that occur during database access or update
                _response.IsSuccess = false;
                _response.Message = $"An error occurred: {ex.Message}";
            }

            // Return the response object containing the operation outcome (success or failure)
            return _response;
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Delete(int id)
        {
            try
            {
                // Retrieve the Product entity with the specified ID from the database
                Product obj = _db.Products.First(u => u.ProductId == id);

                if (obj == null)
                {
                    // If the specified Product is not found, set IsSuccess to false
                    _response.IsSuccess = false;
                    _response.Message = $"Product with ID {id} not found.";
                }
                else
                {
                    // Remove the retrieved Product entity from the database context
                    _db.Products.Remove(obj);

                    // Save changes to persist the deletion
                    _db.SaveChanges();

                    // Indicate that the operation was successful
                    _response.IsSuccess = true;
                    _response.Message = $"Product with ID {id} has been deleted.";
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
