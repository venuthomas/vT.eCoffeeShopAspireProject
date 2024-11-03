using Microsoft.EntityFrameworkCore;
using vT.eCoffeeShop.Infrastructure.Models;

namespace vT.eCoffeeShop.Infrastructure.Contexts.OrderContexts;

public class PostgreSqlDbContextOrder : DbContext
{
    public PostgreSqlDbContextOrder(DbContextOptions<PostgreSqlDbContextOrder> options) : base(options)
    {
    }

    public DbSet<OrdersDto> Orders { get; set; }
    public DbSet<OrderItemDto> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderItemDto>()
            .ToTable("OrderItems", "cfe");

        modelBuilder.Entity<OrdersDto>()
            .ToTable("Orders", "cfe");
    }
}