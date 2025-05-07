using Domain.Entities.Users;
using Infrastructure.Context;
using Infrastructure.Dbo.User;
using Infrastructure.Storage.Base;

namespace Infrastructure.Storage.Users;

public class UserStorage(AppDbContext context, IRequestContext requestContext)
    : StorageBase<User, UserDbo>(context, requestContext)
{
    protected override Task MapEntityFromDboAsync(User entity, UserDbo dbo)
    {
        entity.Id = dbo.Id;
        entity.Name = dbo.Name;
        entity.Surname = dbo.Surname;
        entity.Patronymic = dbo.Patronymic;
        entity.Email = dbo.Email;
        entity.OrganizationName = dbo.OrganizationName;
        entity.OrganizationId = dbo.OrganizationId;
        entity.UserRole = dbo.UserRole;
        entity.PasswordHash = dbo.PasswordHash;
        entity.RefreshToken = dbo.RefreshToken;
        entity.RefreshTokenExpiry = dbo.RefreshTokenExpiry;

        return Task.CompletedTask;
    }

    protected override Task MapDboFromEntityAsync(User entity, UserDbo dbo)
    {
        dbo.Id = entity.Id;
        dbo.Name = entity.Name;
        dbo.Surname = entity.Surname;
        dbo.Patronymic = entity.Patronymic;
        dbo.Email = entity.Email;
        dbo.OrganizationName = entity.OrganizationName;
        dbo.OrganizationId = entity.OrganizationId;
        dbo.UserRole = entity.UserRole;
        dbo.PasswordHash = entity.PasswordHash;
        dbo.RefreshTokenExpiry = entity.RefreshTokenExpiry;
        dbo.RefreshToken = entity.RefreshToken;

        return Task.CompletedTask;
    }

    protected override Task MapDboFromEntityAsync(User? existingEntity,
        User newEntity, UserDbo dbo)
    {
        dbo.Id = newEntity.Id;
        dbo.Name = newEntity.Name;
        dbo.Surname = newEntity.Surname;
        dbo.Patronymic = newEntity.Patronymic;
        dbo.Email = newEntity.Email;
        dbo.OrganizationName = newEntity.OrganizationName;
        dbo.OrganizationId = newEntity.OrganizationId;
        dbo.UserRole = newEntity.UserRole;
        dbo.PasswordHash = newEntity.PasswordHash;
        dbo.RefreshTokenExpiry = newEntity.RefreshTokenExpiry;
        dbo.RefreshToken = newEntity.RefreshToken;

        return Task.CompletedTask;
    }
}