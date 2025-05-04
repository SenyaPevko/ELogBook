using Domain;
using Domain.Entities.Users;
using Domain.Models.ErrorInfo;
using Domain.RequestArgs.SearchRequest;
using Domain.Storage;
using Infrastructure.Helpers.SearchRequestHelper;

namespace Infrastructure.Repository;

public class UserRepository(IStorage<User> storage) : RepositoryBase<User, InvalidUserReason>(storage)
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
}