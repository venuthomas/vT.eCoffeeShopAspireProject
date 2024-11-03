using Microsoft.AspNetCore.Mvc;
using vT.eCoffeeShop.Domain.Models;
using vT.eCoffeeShop.OrderService.Services;

namespace vT.eCoffeeShop.OrderService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly CustomerService _customerService;

    public CustomerController(CustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpPost("save")]
    public async Task<IActionResult> SaveCustomer([FromBody] CustumersModel customerDto)
    {
        var result = await _customerService.SaveCustomerAsync(customerDto);

        return Ok(new { id = result });
    }
}