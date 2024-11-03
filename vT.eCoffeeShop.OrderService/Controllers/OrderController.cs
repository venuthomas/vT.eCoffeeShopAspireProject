using Microsoft.AspNetCore.Mvc;
using vT.eCoffeeShop.Domain.Models;
using vT.eCoffeeShop.OrderService.Services;

namespace vT.eCoffeeShop.OrderService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly OrderItemService _orderService;

    public OrderController(OrderItemService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost("place-order")]
    public async Task<IActionResult> PlaceOrder([FromBody] OrdersModel orderDto)
    {
        var result = await _orderService.PlaceOrderAsync(orderDto);

        return result != null
            ? Ok(new { isSuccess = true, message = "Order placed successfully.", id = result })
            : StatusCode(500, "Failed to place order.");
    }
}