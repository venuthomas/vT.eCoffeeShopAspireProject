using System.ComponentModel.DataAnnotations;

namespace vT.eCoffeeShop.Infrastructure.Models;

public class OrderItemDto
{
    [Key] public Guid OrderItemsId { get; set; }

    [Required] public Guid OrdersId { get; set; }

    [Required] public Guid CoffeeItemId { get; set; }

    [Required] public required string Name { get; set; }

    [Required] public decimal Price { get; set; }

    [Required] public int Quantity { get; set; }
    //[ForeignKey("OrdersId")]
    //public OrdersDto Orders { get; set; }
}