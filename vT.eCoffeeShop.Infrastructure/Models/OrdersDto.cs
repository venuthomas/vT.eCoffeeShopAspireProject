using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using vT.eCoffeeShop.Domain.Enum;

namespace vT.eCoffeeShop.Infrastructure.Models;

public class OrdersDto
{
    [Key] public Guid OrdersId { get; set; }

    [Required] public Guid CustomerId { get; set; }

    [Required] public required string CustomerName { get; set; }

    [Required] public DateTime OrderDate { get; set; }

    [Required] public OrderStatusEnum OrderStatus { get; set; }

    [Required] public ICollection<OrderItemDto>? OrderItems { get; set; }

    [Required] public int TotalQty { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }

    [Required] public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}