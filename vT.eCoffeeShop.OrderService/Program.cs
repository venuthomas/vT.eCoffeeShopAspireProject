using MassTransit;
using Microsoft.AspNetCore.Builder;
using vT.eCoffeeShop.Infrastructure.Contexts.OrderContexts;
using vT.eCoffeeShop.Messaging.EventBus;
using vT.eCoffeeShop.OrderService.Profiles;
using vT.eCoffeeShop.OrderService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add default services
builder.AddServiceDefaults();

// Configure SQL Server and PostgreSQL DbContexts
builder.AddSqlServerDbContext<SqlDbContextOrder>("coffee-sqldb");
builder.AddNpgsqlDbContext<PostgreSqlDbContextOrder>("coffee-postgresdb");

// Configure MassTransit with RabbitMQ
builder.Services.AddMassTransit(x =>
{
    x.AddHealthChecks();
    x.AddLogging();
    x.SetKebabCaseEndpointNameFormatter();
    x.AddPublishMessageScheduler();
    x.UsingRabbitMq((context, cfg) =>
    {
        var configuration = context.GetRequiredService<IConfiguration>();
        var host = configuration.GetConnectionString("coffee-rabbitmq-server");
        cfg.Host(host);
        cfg.ConfigureEndpoints(context);
        if (host != null) x.AddMessageScheduler(new Uri(host));
        cfg.UsePublishMessageScheduler();
    });
});

// Register services
builder.Services.AddScoped<OrderItemService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<IMessagesEventBus, RabbitMqEventBus>();
builder.Services.AddAutoMapper(typeof(OrderProfiles));
builder.AddRedisOutputCache("coffeeRedis");
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Build the application
var app = builder.Build();

// Use default endpoints and configure Swagger in Development
app.MapDefaultEndpoints();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    /*
    // Seed SQL Database
    using (var scope = app.Services.CreateScope())
    {
        var sqlContext = scope.ServiceProvider.GetRequiredService<SQLDbContext>();
        _ = sqlContext.Database.EnsureCreatedAsync();

        if (!sqlContext.CoffeeItems.Any())
        {
            sqlContext.CoffeeItems.AddRange(new[]
            {
                new CoffeeItemDto { CoffeeItemID = Guid.NewGuid(), Name = "Espresso", Description = "Strong and bold coffee.", Price = 102.50m, Weight = 60m, IsAvailable = true, ImageUrl= "assets/img/Espresso.jpg" },
                new CoffeeItemDto { CoffeeItemID = Guid.NewGuid(), Name = "Latte", Description = "Smooth coffee with milk.", Price = 123.50m, Weight = 240m, IsAvailable = true, ImageUrl = "assets/img/Latte.jpg" },
                new CoffeeItemDto { CoffeeItemID = Guid.NewGuid(), Name = "Cappuccino", Description = "Espresso with steamed milk foam.", Price = 75.75m, Weight = 190m, IsAvailable = true, ImageUrl = "assets/img/Cappuccino.jpg" },
                new CoffeeItemDto { CoffeeItemID = Guid.NewGuid(), Name = "Americano", Description = "Espresso diluted with hot water.", Price = 112.00m, Weight = 200m, IsAvailable = false, ImageUrl = "assets/img/Americano.jpg" },
                new CoffeeItemDto { CoffeeItemID = Guid.NewGuid(), Name = "Mocha", Description = "Espresso with steamed milk and chocolate syrup.", Price = 144.00m, Weight = 220m, IsAvailable = true, ImageUrl = "assets/img/Mocha.jpg" }
            });
            sqlContext.SaveChanges();
        }
    }

    // Ensure PostgreSQL Database is created
    using (var scope = app.Services.CreateScope())
    {
        var postgreContext = scope.ServiceProvider.GetRequiredService<PostgreSQLDbContext>();
        var tableExists = await postgreContext.Database.ExecuteSqlRawAsync(
             $"SELECT to_regclass('Orders') IS NOT NULL") > 0;

        if (tableExists)
        {
            // Table exists, proceed with migration
            await postgreContext.Database.EnsureCreatedAsync();
        }
    }
    */
}

// Configure the HTTP request pipeline
app.UseHttpsRedirection();
app.UseOutputCache(); // Added output caching
app.UseCors("AllowAllOrigins"); // Enable CORS policy
app.MapControllers(); // Map API controllers

app.Run();