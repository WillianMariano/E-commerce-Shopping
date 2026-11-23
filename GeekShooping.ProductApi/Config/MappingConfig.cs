using AutoMapper;
using GeekShooping.ProductApi.Data.DTOs;
using GeekShooping.ProductApi.Model;
using Microsoft.Extensions.Logging.Abstractions;

namespace GeekShooping.ProductApi.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfing = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, Product>();
                config.CreateMap<Product, ProductDto>();
            });
            return mappingConfing;
        }
    }
}
