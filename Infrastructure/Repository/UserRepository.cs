using Domain;
using Domain.Entities.Organization;
using Domain.Entities.Users;
using Domain.Models.ErrorInfo;
using Domain.Repository;
using Domain.RequestArgs.SearchRequest;
using Domain.Storage;
using Infrastructure.Helpers.SearchRequestHelper;
using Infrastructure.WriteContext;

namespace Infrastructure.Repository;

public class UserRepository(IStorage<User> storage, IStorage<Organization> organizationStorage)
    : RepositoryBase<User, InvalidUserReason>(storage)
{
    public override async Task AddAsync(User user, IWriteContext<InvalidUserReason> writeContext,
        CancellationToken cancellationToken)
    {
        await base.AddAsync(user, writeContext, cancellationToken);
    }

    protected override async Task ValidateCreationAsync(
        User entity, IWriteContext<InvalidUserReason> writeContext, CancellationToken cancellationToken)
    {
        var existingUsers = await SearchAsync(
            new SearchRequest().WhereEquals<User, string>(u => u.Email, entity.Email).SinglePage(),
            cancellationToken);
        if (existingUsers.Count != 0)
            writeContext.AddInvalidData(new ErrorDetail<InvalidUserReason>
            {
                Path = nameof(entity.Email),
                Reason = InvalidUserReason.EmailAlreadyExists,
                Value = entity.Email
            });
    }
    
    protected override async Task AfterWriteAsync(
        User? oldEntity,
        User newEntity,
        IWriteContext<InvalidUserReason> writeContext,
        CancellationToken cancellationToken)
    {
        if (newEntity.OrganizationId is not null)
        {
            var newOrganization = await organizationStorage.GetByIdAsync(newEntity.OrganizationId.Value);
            if (newOrganization is null)
            {
                writeContext.AddInvalidData(new ErrorDetail<InvalidUserReason>()
                {
                    Path = nameof(newEntity.OrganizationId),
                    Reason = InvalidUserReason.ReferenceIsNotFound,
                    Value = newEntity.OrganizationId.ToString()
                });
                return;
            }
            newOrganization.UserIds.Add(newEntity.Id);

            await organizationStorage.UpdateAsync(newOrganization);
        }

        if (oldEntity?.OrganizationId is not null)
        {
            var oldOrganization = await organizationStorage.GetByIdAsync(oldEntity.OrganizationId.Value);
            if (oldOrganization is null)
            {
                return;
            }
            oldOrganization.UserIds.Remove(oldEntity.Id);

            await organizationStorage.UpdateAsync(oldOrganization);
        }
    }
}