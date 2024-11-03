using vT.eCoffeeShop.Domain.Enum;

namespace vT.eCoffeeShop.Domain.Models;

public class OrdersModel
{
    public Guid? OrdersId { get; set; }
    public Guid CustomerId { get; set; }
    public required string CustomerName { get; set; }
    public DateTime? OrderDate { get; set; }
    public OrderStatusEnum OrderStatus { get; set; }
    public List<OrderItemModel>? OrderItems { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}