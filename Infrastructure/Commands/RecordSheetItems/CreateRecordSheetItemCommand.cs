using Domain.Dtos.RecordSheet;
using Domain.Entities.RecordSheet;
using Domain.Repository;
using Domain.RequestArgs.RecordSheetItems;
using Infrastructure.Commands.Base;
using Infrastructure.Context;

namespace Infrastructure.Commands.RecordSheetItems;

public class CreateRecordSheetItemCommand(
    IRepository<RecordSheetItem, InvalidRecordSheetItemReason> repository,
    RequestContext context)
    : CreateCommandBase<RecordSheetItemDto, RecordSheetItem, RecordSheetItemCreationArgs,
        InvalidRecordSheetItemReason>(repository)
{
    protected override async Task<RecordSheetItemDto> MapToDtoAsync(RecordSheetItem entity) => await entity.ToDto();

    protected override Task<RecordSheetItem> MapToEntityAsync(RecordSheetItemCreationArgs args) =>
        Task.FromResult(new RecordSheetItem
        {
            Id = args.Id,
            Date = context.RequestTime.DateTime,
            RecordSheetId = args.RecordSheetId,
            SpecialistId = context.Auth.UserId,
            Directions = args.Directions,
            Deviations = args.Deviations,
        });
}