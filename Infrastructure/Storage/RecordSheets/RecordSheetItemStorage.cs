using Domain.Entities.RecordSheet;
using Domain.Entities.Users;
using Domain.RequestArgs.RecordSheetItems;
using Domain.Storage;
using Infrastructure.Context;
using Infrastructure.Dbo.RecordSheets;
using Infrastructure.Storage.Base;
using MongoDB.Driver;

namespace Infrastructure.Storage.RecordSheets;

public class RecordSheetItemStorage(AppDbContext context, IRequestContext requestContext, IStorage<User> userStorage)
    : StorageBase<RecordSheetItem, RecordSheetItemDbo, RecordSheetItemSearchRequest>(requestContext)
{
    protected override IMongoCollection<RecordSheetItemDbo> Collection => context.RecordSheetItems;

    protected override async Task MapEntityFromDboAsync(RecordSheetItem entity, RecordSheetItemDbo dbo)
    {
        var specialist = await userStorage.GetByIdAsync(dbo.SpecialistId);
        if (dbo.RepresentativeId is not null)
        {
            var representative = await userStorage.GetByIdAsync(dbo.RepresentativeId.Value);
            entity.RepresentativeSignature = representative!.GetSignature();
        }

        if (dbo.ComplianceNoteUserId is not null)
        {
            var complianceNoteUser = await userStorage.GetByIdAsync(dbo.ComplianceNoteUserId.Value);
            entity.ComplianceNoteSignature = complianceNoteUser!.GetSignature();
        }

        entity.Id = dbo.Id;
        entity.Date = dbo.Date;
        entity.Deviations = dbo.Deviations;
        entity.DeviationFilesIds = dbo.DeviationFilesIds;
        entity.Directions = dbo.Directions;
        entity.DirectionFilesIds = dbo.DirectionFilesIds;
        entity.SpecialistSignature = specialist!.GetSignature();
        entity.SpecialistId = dbo.SpecialistId;
        entity.RepresentativeId = dbo.RepresentativeId;
        entity.ComplianceNoteUserId = dbo.ComplianceNoteUserId;
        entity.RecordSheetId = dbo.RecordSheetId;
    }

    protected override Task MapDboFromEntityAsync(RecordSheetItem entity, RecordSheetItemDbo dbo)
    {
        dbo.Id = entity.Id;
        dbo.Date = entity.Date;
        dbo.Deviations = entity.Deviations;
        dbo.DeviationFilesIds = entity.DeviationFilesIds;
        dbo.Directions = entity.Directions;
        dbo.DirectionFilesIds = entity.DirectionFilesIds;
        dbo.SpecialistId = entity.SpecialistId;
        dbo.RepresentativeId = entity.RepresentativeId;
        dbo.ComplianceNoteUserId = entity.ComplianceNoteUserId;
        dbo.RecordSheetId = entity.RecordSheetId;

        return Task.CompletedTask;
    }

    protected override Task MapDboFromEntityAsync(RecordSheetItem? existingEntity, RecordSheetItem newEntity,
        RecordSheetItemDbo dbo)
    {
        dbo.Id = newEntity.Id;
        dbo.Date = newEntity.Date;
        dbo.Deviations = newEntity.Deviations;
        dbo.DeviationFilesIds = newEntity.DeviationFilesIds;
        dbo.Directions = newEntity.Directions;
        dbo.DirectionFilesIds = newEntity.DirectionFilesIds;
        dbo.SpecialistId = newEntity.SpecialistId;
        dbo.RepresentativeId = newEntity.RepresentativeId;
        dbo.ComplianceNoteUserId = newEntity.ComplianceNoteUserId;
        dbo.RecordSheetId = newEntity.RecordSheetId;

        return Task.CompletedTask;
    }
}