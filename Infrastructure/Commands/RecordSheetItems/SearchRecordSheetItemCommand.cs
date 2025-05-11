using Domain.AccessChecker;
using Domain.Dtos.RecordSheet;
using Domain.Entities.RecordSheet;
using Domain.Repository;
using Domain.RequestArgs.SearchRequest;
using Infrastructure.Commands.Base;

namespace Infrastructure.Commands.RecordSheetItems;

public class SearchRecordSheetItemCommand(
    IRepository<RecordSheetItem, InvalidRecordSheetItemReason, RecordSheetItemSearchRequest> repository,
    IAccessChecker<RecordSheetItem> accessChecker)
    : SearchCommandBase<RecordSheetItemDto, RecordSheetItem, InvalidRecordSheetItemReason,
        RecordSheetItemSearchRequest>(repository, accessChecker)
{
    protected override async Task<RecordSheetItemDto> MapToDtoAsync(RecordSheetItem entity) => await entity.ToDto();
}