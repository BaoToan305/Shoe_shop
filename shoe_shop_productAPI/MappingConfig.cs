using AutoMapper;
using shoe_shop_productAPI.Models;
using shoe_shop_productAPI.Models.Dto;

namespace shoe_shop_productAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingCongif = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, Product>();
                config.CreateMap<Product, ProductDto>();
                config.CreateMap<User, UserDto>();
                config.CreateMap<UserDto, User>();
            });

            return mappingCongif;
        }
    }
}
