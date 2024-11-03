using System.ComponentModel.DataAnnotations;

namespace vT.eCoffeeShop.Domain.Models;

public class OrderItemModel
{
    public Guid OrderItemsId { get; set; }
    public Guid OrdersId { get; set; }

    [Required] public Guid CoffeeItemId { get; set; }

    [Required] public string? Name { get; set; }

    [Required] public decimal Price { get; set; }

    [Required] public int Quantity { get; set; }
    //   public OrdersModel Orders { get; set; }
}