using System.Text;
using System.Text.Json.Serialization;
using Domain.Auth;
using Domain.Commands;
using Domain.Entities.ConstructionSite;
using Domain.Entities.Roles;
using Domain.Entities.Users;
using Domain.Models.Auth;
using Domain.Repository;
using Domain.RequestArgs.Auth;
using Domain.Settings;
using Domain.Storage;
using Infrastructure;
using Infrastructure.Auth;
using Infrastructure.Commands.User;
using Infrastructure.Context;
using Infrastructure.Repository;
using Infrastructure.Storage.ConstructionSite;
using Infrastructure.Storage.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

namespace ELogBook;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();

        services.AddScoped<IRequestContext>(_ => RequestContextHolder.Current);
        services.AddSingleton<EntityDboInterceptor>();

        services.AddStorages();
        services.AddRepositories();

        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        services.AddCommands();

        return services;
    }

    public static IServiceCollection AddAuthenticationAndAuthorization(this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings!.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
                };
            });

        services.AddAuthorizationBuilder()
            .AddPolicy("AdminOnly", policy =>
                policy.RequireRole(UserRole.Admin.ToString()));

        return services;
    }

    public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
    {
        var mongoSettings = configuration.GetSection("MongoDb").Get<MongoDbSettings>();

        services.Configure<MongoDbSettings>(configuration.GetSection("MongoDb"));

        services.AddDbContext<AppDbContext>((sp, options) =>
        {
            options.UseMongoDB(
                mongoSettings!.ConnectionString,
                mongoSettings.DatabaseName
            );

            options.AddInterceptors(sp.GetRequiredService<EntityDboInterceptor>());
        });

        services.AddSingleton<IMongoClient>(sp => new MongoClient(mongoSettings!.ConnectionString));
        services.AddSingleton(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(mongoSettings!.DatabaseName);
        });

        services.AddSingleton<MongoIndexInitializer>();
        services.AddHostedService<MongoIndexInitializerHostedService>();

        return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRepository<User, InvalidUserReason>, UserRepository>();
        services.AddScoped<IRepository<ConstructionSite, InvalidConstructionSiteReason>, ConstructionSiteRepository>();

        return services;
    }

    private static IServiceCollection AddStorages(this IServiceCollection services)
    {
        services.AddScoped<IStorage<User>, UserStorage>();
        services.AddScoped<IStorage<ConstructionSite>, ConstructionSiteStorage>();

        return services;
    }

    private static IServiceCollection AddCommands(this IServiceCollection services)
    {
        services.AddUserCommands();

        return services;
    }

    private static IServiceCollection AddUserCommands(this IServiceCollection services)
    {
        services.AddScoped<ICreateCommand<AuthResponse, RegisterRequest, InvalidUserReason>, CreateUserCommand>();
        services.AddScoped<CreateUserCommand>();
        services.AddScoped<LoginUserCommand>();
        services.AddScoped<RefreshUserTokenCommand>();
        services.AddScoped<RevokeUserTokenCommand>();

        return services;
    }
}