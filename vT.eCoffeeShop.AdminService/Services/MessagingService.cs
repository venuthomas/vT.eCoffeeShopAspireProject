using MassTransit;
using vT.eCoffeeShop.Domain.Models;

namespace vT.eCoffeeShop.AdminService.Services;

public class MessagingService : IConsumer<OrdersModel> //: BackgroundService

{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public MessagingService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task Consume(ConsumeContext<OrdersModel> context)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var orderService = scope.ServiceProvider.GetRequiredService<OrderService>();
            //  await Task.Run(
            //     async () =>
            //  {
            await orderService.PlaceOrderAsync(context.Message);
            //   });
        }

        await Task.CompletedTask;
    }

    //protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    //{
    //    while (!stoppingToken.IsCancellationRequested)
    //    {
    //        using (var scope = _serviceScopeFactory.CreateScope())
    //        {
    //            var _messagesEventBus = scope.ServiceProvider.GetRequiredService<IMessagesEventBus>();
    //            await Task.Run(
    //                 async () =>
    //                 {
    //                     _messagesEventBus.Subscribe<OrdersModel>("OrdersQuery", QrderHandler);
    //                 }, stoppingToken);
    //        }
    //        await Task.CompletedTask;
    //    }
    //}

    // private void QrderHandler(OrdersModel orderDto)
    // {
    //     try
    //     {
    //         using (var scope = _serviceScopeFactory.CreateScope())
    //         {
    //             var orderService = scope.ServiceProvider.GetRequiredService<OrderService>();
    //           //  await Task.Run(
    //       //     async () =>
    //          //  {
    //             orderService.PlaceOrder(orderDto);
    //         //   });
    //         }
    //     }
    //     catch (Exception ex)
    //     {
    //         Console.WriteLine($"QrderHandler error: {ex.Message}");
    //         throw ex;
    //     }
    // }
}