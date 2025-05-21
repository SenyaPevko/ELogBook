using Domain.AccessChecker;
using Domain.Dtos.RecordSheet;
using Domain.Entities.RecordSheet;
using Domain.FileStorage;
using Domain.Repository;
using Domain.RequestArgs.RecordSheetItems;
using Infrastructure.Commands.Base;
using Infrastructure.Context;
using MongoDB.Bson;

namespace Infrastructure.Commands.RecordSheetItems;

public class CreateRecordSheetItemCommand(
    IRepository<RecordSheetItem, InvalidRecordSheetItemReason> repository,
    IRequestContext context,
    IAccessChecker<RecordSheetItem> accessChecker,
    IFileStorageService fileStorage)
    : CreateCommandBase<RecordSheetItemDto, RecordSheetItem, RecordSheetItemCreationArgs,
        InvalidRecordSheetItemReason>(repository, accessChecker)
{
    protected override async Task<RecordSheetItemDto> MapToDtoAsync(RecordSheetItem entity)
    {
        return await entity.ToDto(fileStorage);
    }

    protected override Task<RecordSheetItem> MapToEntityAsync(RecordSheetItemCreationArgs args)
    {
        return Task.FromResult(new RecordSheetItem
        {
            Id = args.Id,
            Date = context.RequestTime.DateTime,
            RecordSheetId = args.RecordSheetId,
            SpecialistId = context.Auth.UserId!.Value,
            Directions = args.Directions,
            DirectionFilesIds = args.DirectionFilesIds.Select(i => new ObjectId(i)).ToList(),
            Deviations = args.Deviations,
            DeviationFilesIds = args.DeviationFilesIds.Select(i => new ObjectId(i)).ToList()
        });
    }
}