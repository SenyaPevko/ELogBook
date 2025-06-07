using Domain.AccessChecker;
using Domain.Dtos.RecordSheet;
using Domain.Entities.RecordSheet;
using Domain.FileStorage;
using Domain.Repository;
using Domain.RequestArgs.RecordSheetItems;
using Infrastructure.Commands.Base;
using MongoDB.Bson;

namespace Infrastructure.Commands.RecordSheetItems;

public class UpdateRecordSheetItemCommand(
    IRepository<RecordSheetItem, InvalidRecordSheetItemReason> repository,
    IAccessChecker<RecordSheetItem, RecordSheetItemUpdateArgs> accessChecker,
    IFileStorageService fileStorage)
    : UpdateCommandBase<RecordSheetItemDto, RecordSheetItem, RecordSheetItemUpdateArgs,
        InvalidRecordSheetItemReason>(repository, accessChecker)
{
    protected override async Task<RecordSheetItemDto> MapToDtoAsync(RecordSheetItem entity)
    {
        return await entity.ToDto(fileStorage);
    }

    protected override Task ApplyUpdatesAsync(RecordSheetItem entity, RecordSheetItemUpdateArgs args)
    {
        if (args.Deviations is not null) entity.Deviations = args.Deviations;
        if (args.Directions is not null) entity.Directions = args.Directions;
        if (args.RepresentativeId is not null) entity.RepresentativeId = args.RepresentativeId.Value;
        if (args.ComplianceNoteUserId is not null) entity.ComplianceNoteUserId = args.ComplianceNoteUserId.Value;
        
        if (args.DeviationFilesIds is not null)
        {
            if (args.DeviationFilesIds.Add is not null)
                entity.DeviationFilesIds.AddRange(args.DeviationFilesIds.Add.Select(x=>new ObjectId(x)));
            if (args.DeviationFilesIds.Remove is not null)
                entity.DeviationFilesIds = entity.DeviationFilesIds.Except(args.DeviationFilesIds.Remove.Select(x=>new ObjectId(x))).ToList();
        }
        
        if (args.DirectionFilesIds is not null)
        {
            if (args.DirectionFilesIds.Add is not null)
                entity.DirectionFilesIds.AddRange(args.DirectionFilesIds.Add.Select(x=>new ObjectId(x)));
            if (args.DirectionFilesIds.Remove is not null)
                entity.DirectionFilesIds = entity.DirectionFilesIds.Except(args.DirectionFilesIds.Remove.Select(x=>new ObjectId(x))).ToList();
        }

        return Task.CompletedTask;
    }
}