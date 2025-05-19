using Domain.AccessChecker;
using Domain.Dtos.RecordSheet;
using Domain.Entities.RecordSheet;
using Domain.FileStorage;
using Domain.Repository;
using Domain.RequestArgs.RecordSheetItems;
using Infrastructure.Commands.Base;

namespace Infrastructure.Commands.RecordSheetItems;

public class SearchRecordSheetItemCommand(
    IRepository<RecordSheetItem, InvalidRecordSheetItemReason, RecordSheetItemSearchRequest> repository,
    IAccessChecker<RecordSheetItem> accessChecker,
    IFileStorageService fileStorageService)
    : SearchCommandBase<RecordSheetItemDto, RecordSheetItem, InvalidRecordSheetItemReason,
        RecordSheetItemSearchRequest>(repository, accessChecker)
{
    protected override async Task<RecordSheetItemDto> MapToDtoAsync(RecordSheetItem entity)
    {
        return await entity.ToDto(fileStorageService);
    }
}