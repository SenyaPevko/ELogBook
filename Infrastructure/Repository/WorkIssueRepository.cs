using Domain.Entities.WorkIssues;
using Domain.Models.ErrorInfo;
using Domain.Repository;
using Domain.RequestArgs.WorkIssues;
using Domain.Storage;

namespace Infrastructure.Repository;

public class WorkIssueRepository(
    IStorage<WorkIssue, WorkIssueSearchRequest> storage,
    IStorage<WorkIssueItem> issueItemStorage)
    : RepositoryBase<WorkIssue, InvalidWorkIssueReason, WorkIssueSearchRequest>(storage)
{
    protected override async Task ValidateCreationAsync(
        WorkIssue entity,
        IWriteContext<InvalidWorkIssueReason> writeContext,
        CancellationToken cancellationToken)
    {
        foreach (var item in entity.Items)
            if (await issueItemStorage.GetByIdAsync(item.Id) is null)
                writeContext.AddInvalidData(new ErrorDetail<InvalidWorkIssueReason>
                {
                    Path = nameof(entity.Items),
                    Reason = InvalidWorkIssueReason.ReferenceNotFound,
                    Value = item.Id.ToString()
                });
    }

    protected override Task ValidateUpdateAsync(WorkIssue oldEntity, WorkIssue newEntity,
        IWriteContext<InvalidWorkIssueReason> writeContext,
        CancellationToken cancellationToken)
    {
        // todo: нужно проверять элементы на существование
        return Task.CompletedTask;
    }
}