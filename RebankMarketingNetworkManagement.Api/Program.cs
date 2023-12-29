using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RebankMarketingNetworkManagement.Api.Constants;
using RebankMarketingNetworkManagement.Api.FilterAttributes;
using RebankMarketingNetworkManagement.Api.JsonConverters;
using RebankMarketingNetworkManagement.Api.Swagger;
using RebankMarketingNetworkManagement.Application;
using RebankMarketingNetworkManagement.Infrastructure;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers(options =>
    {
        options.Filters.Add<ApiExceptionFilterAttribute>();
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddHealthChecks();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressInferBindingSourcesForParameters = true;
});

builder.Services.AddCors(builder.Configuration);

builder.Services.AddMemoryCache();

builder.Services.AddAuthentication(builder.Configuration, builder.Environment);
builder.Services.AddAuthorizationWithPolicy();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseCors();

app.UseSwagger(app.Environment);

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/healthz");

app.Run();

internal static class ServiceCollectionExtensions
{
    public static void AddAuthentication(
            this IServiceCollection services,
            IConfiguration configuration,
            IWebHostEnvironment environment)
    {
        _ = services
            .AddAuthentication()
            .AddJwtBearer(AuthenticationSchemas.RebankMarketingNetworkManagement, options =>
            {
                var issuer = configuration["JwtToken:Issuer"];
                var audience = configuration["JwtToken:Audience"];

                options.RequireHttpsMetadata = !environment.IsDevelopment();

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["JwtToken:SecretKey"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    RoleClaimType = ClaimTypes.Role
                };
            });
    }

    public static void AddAuthorizationWithPolicy(
        this IServiceCollection services) =>
        services
        .AddAuthorization(options =>
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .AddAuthenticationSchemes(
                    AuthenticationSchemas.RebankMarketingNetworkManagement)
                .Build();
        });

    public static void AddSwagger(this IServiceCollection services) =>
        services.AddSwaggerGen(options =>
        {
            options.DocumentFilter<SwaggerDocumentFilter>();

            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "RebankMNM API"
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a JWT token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

            options.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["action"]}");

            options.EnableAnnotations();

            var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly)
                    .Where(f => f.Contains("RebankMNM", StringComparison.OrdinalIgnoreCase));

            foreach (var xmlFile in xmlFiles)
                options.IncludeXmlComments(xmlFile);
        });

    public static void AddCors(
        this IServiceCollection services,
        IConfiguration configuration) =>
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins(configuration.GetSection("AllowedOrigins").Get<string[]>())
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
}

internal static class WebAppExtensions
{
    public static void UseSwagger(
        this WebApplication app,
        IWebHostEnvironment environment)
    {
        app.UseSwagger(options =>
        {
            options.RouteTemplate = environment.IsProduction()
                ? "/RebankMNM-swagger-prod/{documentname}/swagger.json"
                : "/RebankMNM-swagger/{documentname}/swagger.json";

            if (!environment.IsDevelopment())
                options.PreSerializeFilters.Add((swaggerDoc, _) =>
                {
                    swaggerDoc.Servers = new List<OpenApiServer>
                    {
                        new()
                        {
                            Url = $"{app.Configuration.GetValue<string>("ApiGatewayUrl")}"
                        }
                    };
                });
        });

        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("v1/swagger.json", "Rebank Marketing Network Management API V1");

            options.RoutePrefix = environment.IsProduction()
                ? "RebankMNM-swagger-prod"
                : "RebankMNM-swagger";
        });
    }
}