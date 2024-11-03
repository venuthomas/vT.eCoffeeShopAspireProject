using System.ComponentModel.DataAnnotations;

namespace vT.eCoffeeShop.Infrastructure.Models;

public class CustomerDto
{
    [Key] public Guid CustomerId { get; set; }

    [Required] public required string CustomerName { get; set; }
}