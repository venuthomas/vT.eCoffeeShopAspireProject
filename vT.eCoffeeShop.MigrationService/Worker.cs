using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using OpenTelemetry.Trace;
using vT.eCoffeeShop.Infrastructure.Contexts.AdminContexts;
using vT.eCoffeeShop.Infrastructure.Contexts.OrderContexts;

namespace vT.eCoffeeShop.MigrationService;

public class Worker(
    IServiceProvider serviceProvider,
    IHostApplicationLifetime hostApplicationLifetime) : BackgroundService
{
    public const string ActivitySourceName = "Migrations";
    private static readonly ActivitySource SActivitySource = new(ActivitySourceName);

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var activity = SActivitySource.StartActivity("Migrating database", ActivityKind.Client);

        try
        {
            using var scope = serviceProvider.CreateScope();
            await MigrateDatabaseAsync<SqlDbContextOrder>(scope.ServiceProvider, cancellationToken);
            await MigrateDatabaseAsync<PostgreSqlDbContextOrder>(scope.ServiceProvider, cancellationToken);
            await MigrateDatabaseAsync<PostgreSqlDbContextAdmin>(scope.ServiceProvider, cancellationToken);
        }
        catch (Exception ex)
        {
            activity?.RecordException(ex);
            throw;
        }

        hostApplicationLifetime.StopApplication();
    }

    private static async Task MigrateDatabaseAsync<TDbContext>(IServiceProvider serviceProvider,
        CancellationToken cancellationToken)
        where TDbContext : DbContext
    {
        var dbContext = serviceProvider.GetRequiredService<TDbContext>();
        var dbCreator = dbContext.GetService<IRelationalDatabaseCreator>();
        var strategy = dbContext.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            // Ensure the database is created if it does not exist.
            if (!await dbCreator.ExistsAsync(cancellationToken)) await dbCreator.CreateAsync(cancellationToken);

            // Run migration within a transaction.
            await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
            await dbContext.Database.MigrateAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        });
    }
}