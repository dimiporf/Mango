using AutoMapper;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dto;

namespace Mango.Services.ProductAPI
{
    public class MappingConfig
    {
        // Register mapping configurations for AutoMapper
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                // Define mapping from CouponDto to Coupon
                config.CreateMap<ProductDto, Product>().ReverseMap();

                // Also reverse mapping from Coupon to CouponDto
                
            });

            return mappingConfig;
        }
    }
}
