using Microsoft.EntityFrameworkCore;
using vT.eCoffeeShop.Infrastructure.Models;

namespace vT.eCoffeeShop.Infrastructure.Contexts.OrderContexts;

public class SqlDbContextOrder : DbContext
{
    public SqlDbContextOrder(DbContextOptions<SqlDbContextOrder> options) : base(options)
    {
    }

    public DbSet<CoffeeItemDto> CoffeeItems { get; set; }

    public DbSet<CustomerDto> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CoffeeItemDto>()
            .ToTable("CoffeeItems", "cfe");

        modelBuilder.Entity<CustomerDto>()
            .ToTable("Customers", "cfe");

        //  Seed data will be added here
        modelBuilder.Entity<CoffeeItemDto>().HasData(
            new CoffeeItemDto
            {
                CoffeeItemId = Guid.NewGuid(), Name = "Espresso", Description = "Strong and bold coffee.",
                Price = 102.50m, Weight = 60m, IsAvailable = true, ImageUrl = "assets/img/Espresso.jpg"
            },
            new CoffeeItemDto
            {
                CoffeeItemId = Guid.NewGuid(), Name = "Latte", Description = "Smooth coffee with milk.",
                Price = 123.50m, Weight = 240m, IsAvailable = true, ImageUrl = "assets/img/Latte.jpg"
            },
            new CoffeeItemDto
            {
                CoffeeItemId = Guid.NewGuid(), Name = "Cappuccino", Description = "Espresso with steamed milk foam.",
                Price = 75.75m, Weight = 190m, IsAvailable = true, ImageUrl = "assets/img/Cappuccino.jpg"
            },
            new CoffeeItemDto
            {
                CoffeeItemId = Guid.NewGuid(), Name = "Americano", Description = "Espresso diluted with hot water.",
                Price = 112.00m, Weight = 200m, IsAvailable = false, ImageUrl = "assets/img/Americano.jpg"
            },
            new CoffeeItemDto
            {
                CoffeeItemId = Guid.NewGuid(), Name = "Mocha",
                Description = "Espresso with steamed milk and chocolate syrup.", Price = 144.00m, Weight = 220m,
                IsAvailable = true, ImageUrl = "assets/img/Mocha.jpg"
            }
        );
    }
}