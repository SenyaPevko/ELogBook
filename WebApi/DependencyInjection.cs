using System.Text;
using Domain.Auth;
using Domain.Entities.ConstructionSite;
using Domain.Entities.Roles;
using Domain.Entities.Users;
using Domain.Settings;
using Domain.Storage;
using Infrastructure;
using Infrastructure.Auth;
using Infrastructure.Context;
using Infrastructure.Storage.ConstructionSite;
using Infrastructure.Storage.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ELogBook;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();

        services.AddScoped<IRequestContext>(_ => RequestContextHolder.Current);
        services.AddSingleton<EntityDboInterceptor>();

        services.AddScoped(typeof(IStorage<User>), typeof(UserStorage));
        services.AddScoped(typeof(IStorage<ConstructionSite>), typeof(ConstructionSiteStorage));
        
        services.AddControllers();

        return services;
    }
    
    public static IServiceCollection AddAuthenticationAndAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
        var secretKey = Encoding.UTF8.GetBytes(jwtSettings!.Secret);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey)
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

        return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        return services;
    }
}