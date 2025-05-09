using Domain.Dtos.RecordSheet;
using Domain.Entities.RecordSheet;
using Domain.Repository;
using Domain.RequestArgs.SearchRequest;
using Infrastructure.Commands.Base;

namespace Infrastructure.Commands.RecordSheetItems;

public class SearchRecordSheetItemCommand(IRepository<RecordSheetItem, InvalidRecordSheetItemReason, RecordSheetItemSearchRequest> repository)
    : SearchCommandBase<RecordSheetItemDto, RecordSheetItem, InvalidRecordSheetItemReason, RecordSheetItemSearchRequest>(repository)
{
    protected override async Task<RecordSheetItemDto> MapToDtoAsync(RecordSheetItem entity) => await entity.ToDto();
}