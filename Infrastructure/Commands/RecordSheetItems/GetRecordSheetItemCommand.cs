using Domain.AccessChecker;
using Domain.Dtos.RecordSheet;
using Domain.Entities.RecordSheet;
using Domain.FileStorage;
using Domain.Repository;
using Infrastructure.Commands.Base;

namespace Infrastructure.Commands.RecordSheetItems;

public class GetRecordSheetItemCommand(
    IRepository<RecordSheetItem> repository,
    IAccessChecker<RecordSheetItem> accessChecker,
    IFileStorageService fileStorageService)
    : GetCommandBase<RecordSheetItemDto, RecordSheetItem>(repository, accessChecker)
{
    protected override async Task<RecordSheetItemDto> MapToDtoAsync(RecordSheetItem entity)
    {
        return await entity.ToDto(fileStorageService);
    }
}