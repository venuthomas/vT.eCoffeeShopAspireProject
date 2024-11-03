using Microsoft.AspNetCore.SignalR;
using vT.eCoffeeShop.Domain.Models;

namespace vT.eCoffeeShop.AdminService.Hub;

public class OrderHub : Microsoft.AspNetCore.SignalR.Hub
{
    public async Task SendOrderNotification(OrdersModel orderMessage)
    {
        await Clients.All.SendAsync("NewOrderRecived", orderMessage);
    }
}