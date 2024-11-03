var builder = DistributedApplication.CreateBuilder(args);

// Configure SQL Server with existing instance
var sql = builder.AddSqlServer("coffee-sqlserver")
    .WithDataVolume()
    .WithHttpEndpoint(port: 7000, targetPort: 1433) // Assuming 1433 is the default SQL Server port
    .AddDatabase("coffee-sqldb"); // Use existing database

// Configure PostgreSQL with existing instance
var postgres = builder.AddPostgres("coffee-postgres")
    .WithDataVolume()
    .WithHttpEndpoint(port: 7001, targetPort: 5432) // Assuming 5432 is the default PostgreSQL port
    .WithPgAdmin(); // Ensure PgAdmin is used for management

// Connect to existing PostgreSQL databases
var postgresdb = postgres.AddDatabase("coffee-postgresdb");
var postgresAdmindb = postgres.AddDatabase("coffee-admin-postgresdb");


builder.AddProject<Projects.vT_eCoffeeShop_MigrationService>("migrations")
    .WithReference(sql)
    .WithReference(postgresdb)
    .WithReference(postgresAdmindb);


// Configure RabbitMQ with existing instance
var rabbitmq = builder.AddRabbitMQ("coffee-rabbitmq-server")
    .WithHttpEndpoint(port: 7002, targetPort: 15672) // Assuming 15672 is the default RabbitMQ management port
    .WithManagementPlugin(); // Use management plugin for RabbitMQ

// Configure Redis with existing instance
var cache = builder.AddRedis("coffeeRedis").WithDataVolume(); // Connect to existing Redis

// Configure the Order Service
var oederservice = builder.AddProject<Projects.vT_eCoffeeShop_OrderService>("orderservice")
    .WithHttpEndpoint(port:7003)
    .WithReference(cache)
    .WithReference(sql)
    .WithReference(postgresdb)
    .WithReference(rabbitmq)
    .WithExternalHttpEndpoints();

// Configure the Admin Service
var adminservice = builder.AddProject<Projects.vT_eCoffeeShop_AdminService>("adminservice")
    .WithHttpEndpoint(port:7004)
    .WithHttpsEndpoint(port: 8000)
    .WithReference(postgresAdmindb)
    .WithReference(rabbitmq)
    .WithExternalHttpEndpoints();

// Configure the Angular Frontend
builder.AddNpmApp("frontend-angular", "../vT.eCoffeeShop.FrontEnd")
    .WithReference(oederservice)
    .WithReference(adminservice)
    .WithHttpEndpoint(port: 7005, env: "PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile(); // This publishes the frontend as a Dockerfile

// Build and run the application
builder.Build().Run();