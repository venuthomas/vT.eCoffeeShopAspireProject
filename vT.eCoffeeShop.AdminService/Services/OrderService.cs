using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using vT.eCoffeeShop.AdminService.Hub;
using vT.eCoffeeShop.Domain.Models;
using vT.eCoffeeShop.Infrastructure.Contexts.AdminContexts;
using vT.eCoffeeShop.Infrastructure.Models;

namespace vT.eCoffeeShop.AdminService.Services;

public class OrderService
{
    private readonly PostgreSqlDbContextAdmin _dbContext;
    private readonly IMapper _mapper;
    private readonly IHubContext<OrderHub> _orderHubContext;

    public OrderService(
        PostgreSqlDbContextAdmin dbContext,
        IMapper mapper,
        IHubContext<OrderHub> orderHubContext)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _orderHubContext = orderHubContext;
    }

    public async Task PlaceOrderAsync(OrdersModel orderDto)
    {
        Console.WriteLine("QrderHandler loaded");
        var orders = _mapper.Map<OrdersDto>(orderDto);

        _dbContext.Orders.Add(orders);
        var result = _dbContext.SaveChanges();
        await _orderHubContext.Clients.All.SendAsync("NewOrderRecived",
            orderDto); // Send order notification to all clients

        Console.WriteLine("QrderHandler Success");
    }

    public async Task<List<OrdersModel>> GetAllOrders()
    {
        var orders = await _dbContext.Orders.Include(x => x.OrderItems).ToListAsync();
        //         return orders;
        return _mapper.Map<List<OrdersModel>>(orders);
    }
}