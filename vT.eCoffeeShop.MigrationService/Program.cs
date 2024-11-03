using vT.eCoffeeShop.Infrastructure.Contexts.AdminContexts;
using vT.eCoffeeShop.Infrastructure.Contexts.OrderContexts;
using vT.eCoffeeShop.MigrationService;

var builder = Host.CreateApplicationBuilder(args);
builder.AddServiceDefaults();
builder.Services.AddHostedService<Worker>();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

builder.AddSqlServerDbContext<SqlDbContextOrder>("coffee-sqldb");
builder.AddNpgsqlDbContext<PostgreSqlDbContextOrder>("coffee-postgresdb");

builder.AddNpgsqlDbContext<PostgreSqlDbContextAdmin>("coffee-admin-postgresdb");
var host = builder.Build();
host.Run();