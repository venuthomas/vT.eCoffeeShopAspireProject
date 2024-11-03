using System.ComponentModel.DataAnnotations;

namespace vT.eCoffeeShop.Infrastructure.Models;

public class CoffeeItemDto
{
    [Key] public Guid CoffeeItemId { get; set; }

    [Required] public required string Name { get; set; }

    [Required] public string? Description { get; set; }

    [Required] public decimal Price { get; set; }

    [Required] public decimal Weight { get; set; }

    public bool IsAvailable { get; set; }
    public string? ImageUrl { get; set; }
}