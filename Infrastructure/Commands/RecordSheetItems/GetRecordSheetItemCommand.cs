using Domain.Dtos.RecordSheet;
using Domain.Entities.RecordSheet;
using Domain.Repository;
using Infrastructure.Commands.Base;

namespace Infrastructure.Commands.RecordSheetItems;

public class GetRecordSheetItemCommand(IRepository<RecordSheetItem> repository)
    : GetCommandBase<RecordSheetItemDto, RecordSheetItem>(repository)
{
    protected override async Task<RecordSheetItemDto> MapToDtoAsync(RecordSheetItem entity) => await entity.ToDto();
}