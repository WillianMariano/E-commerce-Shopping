using AutoMapper;
using GeekShooping.CouponApi.Model;
using GeekSooping.CouponApi.Data.DTOs;

namespace GeekShooping.CouponApi.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfing = new MapperConfiguration(config =>
            {
                config.CreateMap<CouponDto, Coupon>().ReverseMap(); 
            });
            return mappingConfing;
        }
    }
}
