using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RebankMarketingNetworkManagement.Application.Common.Interfaces.Repositories.Distributor;
using RebankMarketingNetworkManagement.Application.Common.Interfaces.Repositories.Product;
using RebankMarketingNetworkManagement.Application.Common.Interfaces.Repositories.User;
using RebankMarketingNetworkManagement.Application.Common.Interfaces.UnitOfWork;
using RebankMarketingNetworkManagement.Infrastructure.Persistance.Interceptors;
using RebankMarketingNetworkManagement.Infrastructure.Persistance.Repositories.Distributor;
using RebankMarketingNetworkManagement.Infrastructure.Persistance.Repositories.Product;
using RebankMarketingNetworkManagement.Infrastructure.Persistance.Repositories.User;
using RebankMarketingNetworkManagement.Infrastructure.Persistance.UnitOfWork;

namespace RebankMarketingNetworkManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDatabaseRelatedServices(configuration);

        return services;
    }

    private static void AddDatabaseRelatedServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IDistributorAddressInformationRepository, DistributorAddressInformationRepository>()
            .AddScoped<IDistributorContactInformationRepository, DistributorContactInformationRepository>()
            .AddScoped<IDistributorPrivateDocumentInformationRepository, DistributorPrivateDocumentInformationRepository>()
            .AddScoped<IDistributorRepository, DistributorRepository>()
            .AddScoped<IDistributorSaleRepository, DistributorSaleRepository>()
            .AddScoped<IDistributorBonusRepository, DistributorBonusRepository>()
            .AddScoped<IProductRepository, ProductRepository>()
            .AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("AppDbConnection"), builder =>
            {
                builder.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
                       .MigrationsHistoryTable("_RebankMNMDbMigrationHistory", "dbo");
            });

            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });
    }
}
