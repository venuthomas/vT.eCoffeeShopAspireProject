using Microsoft.EntityFrameworkCore;
using vT.eCoffeeShop.Infrastructure.Models;

namespace vT.eCoffeeShop.Infrastructure.Contexts.AdminContexts;

public class PostgreSqlDbContextAdmin : DbContext
{
    public PostgreSqlDbContextAdmin(DbContextOptions<PostgreSqlDbContextAdmin> options) : base(options)
    {
    }

    public DbSet<OrdersDto> Orders { get; set; }
    public DbSet<OrderItemDto> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderItemDto>()
            .ToTable("OrderItems", "adm");

        modelBuilder.Entity<OrdersDto>()
            .ToTable("Orders", "adm");
    }
}