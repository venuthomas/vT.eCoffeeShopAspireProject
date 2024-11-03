using AutoMapper;
using MassTransit;
using vT.eCoffeeShop.Domain.Models;
using vT.eCoffeeShop.Infrastructure.Contexts.OrderContexts;
using vT.eCoffeeShop.Infrastructure.Models;

namespace vT.eCoffeeShop.OrderService.Services;

public class OrderItemService
{
    private readonly PostgreSqlDbContextOrder _dbContext;

    private readonly IMapper _mapper;

    // private readonly IMessagesEventBus _messagesEventBus;
    private readonly IBus _messagesEventBus;

    public OrderItemService(PostgreSqlDbContextOrder dbContext,
        IMapper mapper,
        // IMessagesEventBus messagesEventBus)
        IBus messagesEventBus)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _messagesEventBus = messagesEventBus;
    }

    public async Task<Guid?> PlaceOrderAsync(OrdersModel orderDto)
    {
        try
        {
            orderDto.OrdersId = Guid.NewGuid();
            orderDto.OrderDate = DateTime.UtcNow;
            orderDto.CreatedAt = DateTime.UtcNow;
            orderDto.UpdatedAt = DateTime.UtcNow;
            if (orderDto.OrderItems != null)
                foreach (var item in orderDto.OrderItems)
                {
                    item.OrderItemsId = Guid.NewGuid();
                    item.OrdersId = orderDto.OrdersId ?? Guid.NewGuid();
                }

            var orders = _mapper.Map<OrdersDto>(orderDto);
            {
                _dbContext.Orders.Add(orders);
                await _dbContext.SaveChangesAsync();

                //   bool result = await _messagesEventBus.PublishAsync("OrdersQuery", orderDto);
                await _messagesEventBus.Publish(orderDto);
                return orders.OrdersId;
            }
        }
        catch (Exception)
        {
            return default;
        }
    }
}