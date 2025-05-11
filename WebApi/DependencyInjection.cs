using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using Domain.AccessChecker;
using Domain.Auth;
using Domain.Commands;
using Domain.Dtos;
using Domain.Dtos.RecordSheet;
using Domain.Dtos.RegistrationSheet;
using Domain.Dtos.WorkIssue;
using Domain.Entities.ConstructionSite;
using Domain.Entities.Organization;
using Domain.Entities.RecordSheet;
using Domain.Entities.RegistrationSheet;
using Domain.Entities.Roles;
using Domain.Entities.Users;
using Domain.Entities.WorkIssues;
using Domain.Models.Auth;
using Domain.Repository;
using Domain.RequestArgs.Auth;
using Domain.RequestArgs.ConstructionSites;
using Domain.RequestArgs.Organizations;
using Domain.RequestArgs.RecordSheetItems;
using Domain.RequestArgs.RegistrationSheetItems;
using Domain.RequestArgs.SearchRequest;
using Domain.RequestArgs.Users;
using Domain.RequestArgs.WorkIssueItems;
using Domain.Settings;
using Domain.Storage;
using ELogBook.Handlers;
using Infrastructure;
using Infrastructure.AccessCheckers.ConstructionSites;
using Infrastructure.AccessCheckers.Organizations;
using Infrastructure.AccessCheckers.RecordSheets;
using Infrastructure.AccessCheckers.RegistrationSheets;
using Infrastructure.AccessCheckers.Users;
using Infrastructure.AccessCheckers.WorkIssues;
using Infrastructure.Auth;
using Infrastructure.Commands.ConstructionSites;
using Infrastructure.Commands.Organizations;
using Infrastructure.Commands.RecordSheetItems;
using Infrastructure.Commands.RegistrationSheetItems;
using Infrastructure.Commands.Users;
using Infrastructure.Commands.WorkIssueItems;
using Infrastructure.Context;
using Infrastructure.Repository;
using Infrastructure.Storage.ConstructionSites;
using Infrastructure.Storage.Organizations;
using Infrastructure.Storage.RecordSheets;
using Infrastructure.Storage.RegistrationSheets;
using Infrastructure.Storage.Users;
using Infrastructure.Storage.WorkIssues;
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
        services.AddAccessCheckers();

        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            options.JsonSerializerOptions.DefaultIgnoreCondition =
                JsonIgnoreCondition.WhenWritingNull;
            options.JsonSerializerOptions.DefaultIgnoreCondition =
                JsonIgnoreCondition.WhenWritingDefault;
        });

        services.AddCommands();

        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

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

    private static IServiceCollection AddStorages(this IServiceCollection services)
    {
        services.AddScoped<IStorage<User>, UserStorage>();
        services.AddScoped<IStorage<User, UserSearchRequest>, UserStorage>();

        services.AddScoped<IStorage<ConstructionSite>, ConstructionSiteStorage>();
        services.AddScoped<IStorage<ConstructionSite, ConstructionSiteSearchRequest>, ConstructionSiteStorage>();

        services.AddScoped<IStorage<Organization>, OrganizationStorage>();
        services.AddScoped<IStorage<Organization, OrganizationSearchRequest>, OrganizationStorage>();

        services.AddScoped<IStorage<RegistrationSheetItem>, RegistrationSheetItemStorage>();
        services
            .AddScoped<IStorage<RegistrationSheetItem, RegistrationSheetItemSearchRequest>,
                RegistrationSheetItemStorage>();

        services.AddScoped<IStorage<RegistrationSheet>, RegistrationSheetStorage>();
        services.AddScoped<IStorage<RegistrationSheet, RegistrationSheetSearchRequest>, RegistrationSheetStorage>();

        services.AddScoped<IStorage<RecordSheet>, RecordSheetStorage>();
        services.AddScoped<IStorage<RecordSheet, RecordSheetSearchRequest>, RecordSheetStorage>();

        services.AddScoped<IStorage<RecordSheetItem>, RecordSheetItemStorage>();
        services.AddScoped<IStorage<RecordSheetItem, RecordSheetItemSearchRequest>, RecordSheetItemStorage>();

        services.AddScoped<IStorage<WorkIssue>, WorkIssueStorage>();
        services.AddScoped<IStorage<WorkIssue, WorkIssueSearchRequest>, WorkIssueStorage>();

        services.AddScoped<IStorage<WorkIssueItem>, WorkIssueItemStorage>();
        services.AddScoped<IStorage<WorkIssueItem, WorkIssueItemSearchRequest>, WorkIssueItemStorage>();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRepository<User, InvalidUserReason>, UserRepository>();
        services.AddScoped<IRepository<User, InvalidUserReason, UserSearchRequest>, UserRepository>();
        services.AddScoped<IRepository<User>, UserRepository>();

        services.AddScoped<IRepository<ConstructionSite>, ConstructionSiteRepository>();
        services.AddScoped<IRepository<ConstructionSite, InvalidConstructionSiteReason>, ConstructionSiteRepository>();
        services
            .AddScoped<IRepository<ConstructionSite, InvalidConstructionSiteReason, ConstructionSiteSearchRequest>,
                ConstructionSiteRepository>();

        services.AddScoped<IRepository<Organization>, OrganizationRepository>();
        services.AddScoped<IRepository<Organization, InvalidOrganizationReason>, OrganizationRepository>();
        services
            .AddScoped<IRepository<Organization, InvalidOrganizationReason, OrganizationSearchRequest>,
                OrganizationRepository>();

        services.AddScoped<IRepository<RegistrationSheetItem>, RegistrationSheetItemRepository>();
        services
            .AddScoped<IRepository<RegistrationSheetItem, InvalidRegistrationSheetItemReason>,
                RegistrationSheetItemRepository>();
        services
            .AddScoped<IRepository<RegistrationSheetItem, InvalidRegistrationSheetItemReason,
                RegistrationSheetItemSearchRequest>, RegistrationSheetItemRepository>();

        services.AddScoped<IRepository<RegistrationSheet>, RegistrationSheetRepository>();
        services
            .AddScoped<IRepository<RegistrationSheet, InvalidRegistrationSheetReason>, RegistrationSheetRepository>();
        services
            .AddScoped<IRepository<RegistrationSheet, InvalidRegistrationSheetReason, RegistrationSheetSearchRequest>,
                RegistrationSheetRepository>();

        services.AddScoped<IRepository<RecordSheet>, RecordSheetRepository>();
        services.AddScoped<IRepository<RecordSheet, InvalidRecordSheetReason>, RecordSheetRepository>();
        services
            .AddScoped<IRepository<RecordSheet, InvalidRecordSheetReason, RecordSheetSearchRequest>,
                RecordSheetRepository>();

        services.AddScoped<IRepository<RecordSheetItem>, RecordSheetItemRepository>();
        services.AddScoped<IRepository<RecordSheetItem, InvalidRecordSheetItemReason>, RecordSheetItemRepository>();
        services
            .AddScoped<IRepository<RecordSheetItem, InvalidRecordSheetItemReason, RecordSheetItemSearchRequest>,
                RecordSheetItemRepository>();

        services.AddScoped<IRepository<WorkIssue>, WorkIssueRepository>();
        services.AddScoped<IRepository<WorkIssue, InvalidWorkIssueReason>, WorkIssueRepository>();
        services
            .AddScoped<IRepository<WorkIssue, InvalidWorkIssueReason, WorkIssueSearchRequest>, WorkIssueRepository>();

        services.AddScoped<IRepository<WorkIssueItem>, WorkIssueItemRepository>();
        services.AddScoped<IRepository<WorkIssueItem, InvalidWorkIssueItemReason>, WorkIssueItemRepository>();
        services
            .AddScoped<IRepository<WorkIssueItem, InvalidWorkIssueItemReason, WorkIssueItemSearchRequest>,
                WorkIssueItemRepository>();

        return services;
    }

    private static IServiceCollection AddAccessCheckers(this IServiceCollection services)
    {
        services.AddScoped<IAccessChecker<User>, UserAccessChecker>();
        services.AddScoped<IAccessChecker<User, UserUpdateArgs>, UserAccessChecker>();

        services.AddScoped<IAccessChecker<ConstructionSite>, ConstructionSiteAccessChecker>();
        services
            .AddScoped<IAccessChecker<ConstructionSite, ConstructionSiteUpdateArgs>, ConstructionSiteAccessChecker>();

        services.AddScoped<IAccessChecker<Organization>, OrganizationAccessChecker>();
        services.AddScoped<IAccessChecker<Organization, OrganizationUpdateArgs>, OrganizationAccessChecker>();

        services.AddScoped<IAccessChecker<RecordSheet>, RecordSheetAccessChecker>();

        services.AddScoped<IAccessChecker<RecordSheetItem>, RecordSheetItemAccessChecker>();
        services.AddScoped<IAccessChecker<RecordSheetItem, RecordSheetItemUpdateArgs>, RecordSheetItemAccessChecker>();

        services.AddScoped<IAccessChecker<RegistrationSheet>, RegistrationSheetAccessChecker>();

        services.AddScoped<IAccessChecker<RegistrationSheetItem>, RegistrationSheetItemAccessChecker>();
        services
            .AddScoped<IAccessChecker<RegistrationSheetItem, RegistrationSheetItemUpdateArgs>,
                RegistrationSheetItemAccessChecker>();

        services.AddScoped<IAccessChecker<WorkIssueItem>, WorkIssueItemAccessChecker>();
        services.AddScoped<IAccessChecker<WorkIssueItem, WorkIssueItemUpdateArgs>, WorkIssueItemAccessChecker>();

        services.AddScoped<IAccessChecker<WorkIssue>, WorkIssueAccessChecker>();

        return services;
    }

    private static IServiceCollection AddCommands(this IServiceCollection services)
    {
        services.AddUserCommands();
        services.AddConstructionSiteCommands();
        services.AddOrganizationCommands();
        services.AddRegistrationSheetItemCommands();
        services.AddRecordSheetItemCommands();
        services.AddWorkIssueItemCommands();

        return services;
    }

    private static IServiceCollection AddUserCommands(this IServiceCollection services)
    {
        services.AddScoped<ICreateCommand<AuthResponse, RegisterRequest, InvalidUserReason>, CreateUserCommand>();
        services.AddScoped<IGetCommand<UserDto>, GetUserCommand>();
        services.AddScoped<IUpdateCommand<UserDto, UserUpdateArgs, InvalidUserReason>, UpdateUserCommand>();
        services.AddScoped<ISearchCommand<UserDto, UserSearchRequest>, SearchUserCommand>();
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
        services
            .AddScoped<ISearchCommand<ConstructionSiteDto, ConstructionSiteSearchRequest>, SearchConstructionSite>();

        return services;
    }

    private static IServiceCollection AddOrganizationCommands(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateCommand<OrganizationDto, OrganizationCreationArgs, InvalidOrganizationReason>,
                CreateOrganizationCommand>();
        services.AddScoped<IGetCommand<OrganizationDto>, GetOrganizationCommand>();
        services
            .AddScoped<IUpdateCommand<OrganizationDto, OrganizationUpdateArgs, InvalidOrganizationReason>,
                UpdateOrganizationCommand>();
        services.AddScoped<ISearchCommand<OrganizationDto, OrganizationSearchRequest>, SearchOrganizationCommand>();

        return services;
    }

    private static IServiceCollection AddRegistrationSheetItemCommands(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateCommand<RegistrationSheetItemDto, RegistrationSheetItemCreationArgs,
                    InvalidRegistrationSheetItemReason>,
                CreateRegistrationSheetItemCommand>();
        services.AddScoped<IGetCommand<RegistrationSheetItemDto>, GetRegistrationSheetItemCommand>();
        services
            .AddScoped<IUpdateCommand<RegistrationSheetItemDto, RegistrationSheetItemUpdateArgs,
                InvalidRegistrationSheetItemReason>, UpdateRegistrationSheetItemCommand>();
        services
            .AddScoped<ISearchCommand<RegistrationSheetItemDto, RegistrationSheetItemSearchRequest>,
                SearchRegistrationSheetItemCommand>();

        return services;
    }

    private static IServiceCollection AddRecordSheetItemCommands(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateCommand<RecordSheetItemDto, RecordSheetItemCreationArgs,
                    InvalidRecordSheetItemReason>,
                CreateRecordSheetItemCommand>();
        services.AddScoped<IGetCommand<RecordSheetItemDto>, GetRecordSheetItemCommand>();
        services
            .AddScoped<IUpdateCommand<RecordSheetItemDto, RecordSheetItemUpdateArgs,
                InvalidRecordSheetItemReason>, UpdateRecordSheetItemCommand>();
        services
            .AddScoped<ISearchCommand<RecordSheetItemDto, RecordSheetItemSearchRequest>,
                SearchRecordSheetItemCommand>();

        return services;
    }

    private static IServiceCollection AddWorkIssueItemCommands(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateCommand<WorkIssueItemDto, WorkIssueItemCreationArgs,
                    InvalidWorkIssueItemReason>,
                CreateWorkIssueItemCommand>();
        services.AddScoped<IGetCommand<WorkIssueItemDto>, GetWorkIssueItemCommand>();
        services
            .AddScoped<IUpdateCommand<WorkIssueItemDto, WorkIssueItemUpdateArgs,
                InvalidWorkIssueItemReason>, UpdateWorkIssueItemCommand>();
        services.AddScoped<ISearchCommand<WorkIssueItemDto, WorkIssueItemSearchRequest>, SearchWorkIssueItemCommand>();

        return services;
    }
}