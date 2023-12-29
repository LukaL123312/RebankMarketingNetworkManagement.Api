using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RebankMarketingNetworkManagement.Infrastructure.Persistance.Interceptors;
using System.Reflection;

namespace RebankMarketingNetworkManagement.Infrastructure;

public class AppDbContext : DbContext
{
    private const string DefaultSchema = "dbo";
    private readonly AuditableEntitySaveChangesInterceptor _auditInterceptor;

    public AppDbContext(
        DbContextOptions<AppDbContext> options,
        AuditableEntitySaveChangesInterceptor auditInterceptor)
        : base(options)
    {
        _auditInterceptor = auditInterceptor;
    }

    private static EventId[] IgnoredCoreEvents => new[]
    {
            CoreEventId.QueryCompilationStarting,
            CoreEventId.QueryExecutionPlanned,
            CoreEventId.NavigationBaseIncluded,
            CoreEventId.ValueGenerated,
            CoreEventId.StateChanged
        };

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditInterceptor);
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.EnableDetailedErrors();
        optionsBuilder.ConfigureWarnings(b => b.Ignore(IgnoredCoreEvents));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(DefaultSchema);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
