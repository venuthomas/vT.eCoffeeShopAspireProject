using MassTransit;
using Microsoft.AspNetCore.Builder;
using vT.eCoffeeShop.AdminService.Hub;
using vT.eCoffeeShop.AdminService.Profiles;
using vT.eCoffeeShop.AdminService.Services;
using vT.eCoffeeShop.Infrastructure.Contexts.AdminContexts;
using vT.eCoffeeShop.Messaging.EventBus;

var builder = WebApplication.CreateBuilder(args);

// Fix the CORS policy to specify explicit origins when credentials are used
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", corsBuilder =>
    {
        corsBuilder
            .WithOrigins("http://localhost:4200") // Add specific origins instead of `AllowAnyOrigin`
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials(); // This allows SignalR and other credentials to work
    });
});

builder.AddServiceDefaults();

builder.AddNpgsqlDbContext<PostgreSqlDbContextAdmin>
    ("coffee-admin-postgresdb");

// Configure RabbitMQ for MassTransit
builder.Services.AddMassTransit(x =>
{
    x.AddHealthChecks();
    x.AddLogging();
    x.SetKebabCaseEndpointNameFormatter();
    x.AddConsumer<MessagingService>();
    x.AddPublishMessageScheduler();
    x.UsingRabbitMq((context, cfg) =>
    {
        var configuration = context.GetRequiredService<IConfiguration>();
        var host = configuration.GetConnectionString("coffee-rabbitmq-server");
        cfg.Host(host);
        cfg.ConfigureEndpoints(context);
        cfg.UsePublishMessageScheduler();
    });
});

builder.Services.AddScoped<IMessagesEventBus, RabbitMqEventBus>();
builder.Services.AddAutoMapper(typeof(OrderProfiles));
builder.Services.AddScoped<OrderService>();
builder.Services.AddControllers();
builder.Services.AddProblemDetails();
builder.Services.AddSignalR();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Apply CORS globally (should be placed before routing and endpoints)
app.UseCors("AllowSpecificOrigins");

// Apply Swagger for development environments
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // using (var scope = app.Services.CreateScope())
    // {
    //     var context = scope.ServiceProvider.GetRequiredService<PostgreSqlDbContextAdmin>();
    //     var tableExists = await context.Database.ExecuteSqlRawAsync(
    //            $"SELECT to_regclass('Orders') IS NOT NULL") > 0;
    //
    //     if (tableExists)
    //     {
    //         // Table exists, proceed with migration
    //         await context.Database.EnsureCreatedAsync();
    //     }
    // }
}

// Ensure PostgreSQL database is created if it doesn't exist


app.UseHttpsRedirection();
app.UseRouting();

// Add SignalR Hub routes and controllers
app.UseEndpoints(endpoints =>
{
    if (endpoints != null)
    {
        endpoints.MapControllers();
        endpoints.MapHub<OrderHub>("/api/Hub/orderHub");
    }
});

// Start the app
app.Run();