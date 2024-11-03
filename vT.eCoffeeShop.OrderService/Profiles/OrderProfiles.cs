using AutoMapper;
using vT.eCoffeeShop.Domain.Models;
using vT.eCoffeeShop.Infrastructure.Models;

namespace vT.eCoffeeShop.OrderService.Profiles;

public class OrderProfiles : Profile
{
    public OrderProfiles()
    {
        // Map OrdersDto to Orders
        CreateMap<OrdersModel, OrdersDto>()
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));
        CreateMap<OrdersDto, OrdersModel>()
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));

        //// Map OrderItemDto to OrderItems
        CreateMap<OrderItemDto, OrderItemModel>();
        //    .ForMember(dest => dest.Orders, opt => opt.Ignore()); // Ignore to avoid recursion

        CreateMap<OrderItemModel, OrderItemDto>();


        CreateMap<CustumersModel, CustomerDto>();
    }
}