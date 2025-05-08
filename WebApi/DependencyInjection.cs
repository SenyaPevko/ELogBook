using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using Domain.Auth;
using Domain.Commands;
using Domain.Dtos;
using Domain.Entities.ConstructionSite;
using Domain.Entities.Organization;
using Domain.Entities.Roles;
using Domain.Entities.Users;
using Domain.Models.Auth;
using Domain.Repository;
using Domain.RequestArgs.Auth;
using Domain.RequestArgs.ConstructionSites;
using Domain.RequestArgs.Organizations;
using Domain.RequestArgs.Users;
using Domain.Settings;
using Domain.Storage;
using Infrastructure;
using Infrastructure.Auth;
using Infrastructure.Commands.ConstructionSites;
using Infrastructure.Commands.Organizations;
using Infrastructure.Commands.Users;
using Infrastructure.Context;
using Infrastructure.Repository;
using Infrastructure.Storage.ConstructionSites;
using Infrastructure.Storage.Organizations;
using Infrastructure.Storage.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                    NameClaimType = ClaimTypes.NameIdentifier,
                    RoleClaimType = ClaimTypes.Role
                };
            });

        services.AddAuthorizationBuilder()
            .AddPolicy(AuthPolicy.AdminOnly, policy =>
                policy.RequireRole(UserRole.Admin.ToString()))
            .AddPolicy(AuthPolicy.Authenticated, policy =>
                policy.RequireAuthenticatedUser());

        return services;
    }

    public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

        var mongoSettings = configuration.GetSection("MongoDb").Get<MongoDbSettings>();

        services.Configure<MongoDbSettings>(configuration.GetSection("MongoDb"));
        services.AddSingleton<IMongoClient>(sp => new MongoClient(mongoSettings!.ConnectionString));
        services.AddSingleton(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(mongoSettings!.DatabaseName);
        });

        services.AddSingleton<MongoIndexInitializer>();
        services.AddHostedService<MongoIndexInitializerHostedService>();

        services.AddSingleton<AppDbContext>();

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
        services.AddScoped<IRepository<User>, UserRepository>();

        services.AddScoped<IRepository<ConstructionSite>, ConstructionSiteRepository>();
        services.AddScoped<IRepository<ConstructionSite, InvalidConstructionSiteReason>, ConstructionSiteRepository>();

        services.AddScoped<IRepository<Organization>, OrganizationRepository>();
        services.AddScoped<IRepository<Organization, InvalidOrganizationReason>, OrganizationRepository>();

        return services;
    }

    private static IServiceCollection AddStorages(this IServiceCollection services)
    {
        services.AddScoped<IStorage<User>, UserStorage>();
        services.AddScoped<IStorage<ConstructionSite>, ConstructionSiteStorage>();
        services.AddScoped<IStorage<Organization>, OrganizationStorage>();

        return services;
    }

    private static IServiceCollection AddCommands(this IServiceCollection services)
    {
        services.AddUserCommands();
        services.AddConstructionSiteCommands();
        services.AddOrganizationCommands();

        return services;
    }

    private static IServiceCollection AddUserCommands(this IServiceCollection services)
    {
        services.AddScoped<ICreateCommand<AuthResponse, RegisterRequest, InvalidUserReason>, CreateUserCommand>();
        services.AddScoped<IGetCommand<UserDto>, GetUserCommand>();
        services.AddScoped<IUpdateCommand<UserDto, UserUpdateArgs, InvalidUserReason>, UpdateUserCommand>();
        services.AddScoped<ISearchCommand<UserDto>, SearchUserCommand>();
        services.AddScoped<CreateUserCommand>();
        services.AddScoped<LoginUserCommand>();
        services.AddScoped<RefreshUserTokenCommand>();
        services.AddScoped<RevokeUserTokenCommand>();

        return services;
    }

    private static IServiceCollection AddConstructionSiteCommands(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateCommand<ConstructionSiteDto, ConstructionSiteCreationArgs, InvalidConstructionSiteReason>,
                CreateConstructionSiteCommand>();
        services.AddScoped<IGetCommand<ConstructionSiteDto>, GetConstructionSiteCommand>();
        services
            .AddScoped<IUpdateCommand<ConstructionSiteDto, ConstructionSiteUpdateArgs, InvalidConstructionSiteReason>,
                UpdateConstructionSiteCommand>();
        services.AddScoped<ISearchCommand<ConstructionSiteDto>, SearchConstructionSite>();

        return services;
    }

    private static IServiceCollection AddOrganizationCommands(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateCommand<OrganizationDto, OrganizationCreationArgs, InvalidOrganizationReason>,
                CreateOrganizationCommand>();
        services.AddScoped<IGetCommand<OrganizationDto>, GetOrganizationCommand>();
        services.AddScoped<IUpdateCommand<OrganizationDto, OrganizationUpdateArgs, InvalidOrganizationReason>, UpdateOrganizationCommand>();
        services.AddScoped<ISearchCommand<OrganizationDto>, SearchOrganizationCommand>();

        return services;
    }
}