using Infrastructure.Context;
using Infrastructure.Dbo;
using Infrastructure.Storage.Base;

namespace Infrastructure.Storage.User;

public class UserStorage(AppDbContext context) : StorageBase<Domain.Entities.Users.User, UserDbo>(context)
{
    protected override Task MapEntityFromDboAsync(Domain.Entities.Users.User entity, UserDbo dbo)
    {
        entity.Name = dbo.Name;
        entity.Surname = dbo.Surname;
        entity.Patronymic = dbo.Patronymic;
        entity.Email = dbo.Email;
        entity.OrganizationName = dbo.OrganizationName;
        entity.OrganizationId = dbo.OrganizationId;
        entity.UserRole = dbo.UserRole;

        return Task.CompletedTask;
    }

    protected override Task MapDboFromEntityAsync(Domain.Entities.Users.User newEntity, UserDbo dbo)
    {
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

    protected override Task MapDboFromEntityAsync(Domain.Entities.Users.User? existingEntity,
        Domain.Entities.Users.User newEntity, UserDbo dbo)
    {
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