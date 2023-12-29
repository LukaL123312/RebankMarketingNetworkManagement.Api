using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RebankMarketingNetworkManagement.Application.Common.Behaviours;
using RebankMarketingNetworkManagement.Application.Common.Helpers;
using RebankMarketingNetworkManagement.Application.Common.Interfaces.Helpers;
using RebankMarketingNetworkManagement.Application.Common.Security.Jwt.Interfaces;
using RebankMarketingNetworkManagement.Application.Common.Security.Jwt.Options;
using RebankMarketingNetworkManagement.Application.Common.Security;
using System.Reflection;
using FluentValidation;

namespace RebankMarketingNetworkManagement.Application;

public static class DependencyInjection
{
    public static void AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));

        services.AddOptions(configuration);

        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<ICurrentUser, CurrentUser>();
    }

    public static void AddOptions(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JwtTokenOptions>(options =>
               configuration.GetSection(JwtTokenOptions.Key).Bind(options));
    }
}
