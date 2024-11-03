using System.ComponentModel.DataAnnotations;

namespace vT.eCoffeeShop.Domain.Models;

public class CustumersModel
{
    public Guid? CustomerId { get; set; }

    [Required] public required string CustomerName { get; set; }
}