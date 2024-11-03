using Microsoft.AspNetCore.Mvc;
using vT.eCoffeeShop.AdminService.Services;

namespace vT.eCoffeeShop.AdminService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdminPanelController : ControllerBase
{
    private readonly OrderService _orderService;

    public AdminPanelController(OrderService orderService)
    {
        _orderService = orderService;
    }


    [HttpGet("getallorders")]
    public async Task<IActionResult> GetAllOrders()
    {
        var orders = await _orderService.GetAllOrders();
        return Ok(orders);
    }
}