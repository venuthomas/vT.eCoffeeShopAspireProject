using AutoMapper;
using vT.eCoffeeShop.Domain.Models;
using vT.eCoffeeShop.Infrastructure.Models;

namespace vT.eCoffeeShop.AdminService.Profiles;

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
        CreateMap<OrderItemModel, OrderItemDto>();
        //    .ForMember(dest => dest.Orders, opt => opt.Ignore()); // Ignore to avoid recursion

        CreateMap<OrderItemModel, OrderItemDto>();


        CreateMap<OrderItemDto, OrderItemModel>();
        CreateMap<CustumersModel, CustomerDto>();
    }
}