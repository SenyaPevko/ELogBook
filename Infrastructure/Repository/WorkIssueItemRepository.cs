using Domain;
using Domain.Entities.WorkIssues;
using Domain.Models.ErrorInfo;
using Domain.RequestArgs.SearchRequest;
using Domain.Storage;

namespace Infrastructure.Repository;

public class WorkIssueItemRepository(
    IStorage<WorkIssueItem, WorkIssueItemSearchRequest> storage,
    IStorage<WorkIssue> issueStorage)
    : RepositoryBase<WorkIssueItem, InvalidWorkIssueItemReason, WorkIssueItemSearchRequest>(storage)
{
    protected override async Task ValidateCreationAsync(WorkIssueItem entity,
        IWriteContext<InvalidWorkIssueItemReason> writeContext, CancellationToken cancellationToken)
    {
        var workIssue = await issueStorage.GetByIdAsync(entity.WorkIssueId);
        if (workIssue is null)
            writeContext.AddInvalidData(new ErrorDetail<InvalidWorkIssueItemReason>
            {
                Path = nameof(entity.WorkIssueId),
                Reason = InvalidWorkIssueItemReason.ReferenceNotFound,
                Value = entity.WorkIssueId.ToString()
            });
    }

    protected override Task ValidateUpdateAsync(WorkIssueItem oldEntity, WorkIssueItem newEntity,
        IWriteContext<InvalidWorkIssueItemReason> writeContext,
        CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    protected override async Task AfterCreateAsync(
        WorkIssueItem entity,
        IWriteContext<InvalidWorkIssueItemReason> writeContext,
        CancellationToken cancellationToken)
    {
        var workIssue = await issueStorage.GetByIdAsync(entity.WorkIssueId);
        workIssue!.Items.Add(entity);
        await issueStorage.UpdateAsync(workIssue);
    }
}