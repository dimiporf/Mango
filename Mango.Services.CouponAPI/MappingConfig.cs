using AutoMapper;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;

namespace Mango.Services.CouponAPI
{
    public class MappingConfig
    {
        // Register mapping configurations for AutoMapper
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                // Define mapping from CouponDto to Coupon
                config.CreateMap<CouponDto, Coupon>();

                // Define reverse mapping from Coupon to CouponDto
                config.CreateMap<Coupon, CouponDto>();
            });

            return mappingConfig;
        }
    }
}
