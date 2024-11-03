namespace vT.eCoffeeShop.Domain.Models;

public class CoffeeItemModel
{
    public Guid CoffeeItemId { get; set; }
    public required string Name { get; set; } = "";
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public decimal Weight { get; set; }
    public bool IsAvailable { get; set; }
    public string? ImageUrl { get; set; }
}