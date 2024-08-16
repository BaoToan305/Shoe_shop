using AutoMapper;
using shoe_shop_productAPI.Models.Dto;
using shoe_shop_productAPI.Models;

namespace shoe_shop_productAPI.Mapper
{
    public class OrderDetailMapping: Profile
    {
        public OrderDetailMapping()
        {
            CreateMap<OrderDetail, OrderDetailDto>()
            .ForMember(dest => dest.orders_detail_id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.orders_id, opt => opt.MapFrom(src => src.OrderId))
            .ForMember(dest => dest.product_id, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.product_name, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.quantity, opt => opt.MapFrom(src => src.Quantity));

            // Mapping from OrderDetailDto to OrderDetail
            CreateMap<OrderDetailDto, OrderDetail>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.orders_detail_id))
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.orders_id))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.product_id))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.product_name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.price))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.quantity));
        }
    }
}
