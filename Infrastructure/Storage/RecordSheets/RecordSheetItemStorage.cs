using Domain.Entities.RecordSheet;
using Domain.Entities.Users;
using Domain.Storage;
using Infrastructure.Context;
using Infrastructure.Dbo.RecordSheets;
using Infrastructure.Storage.Base;
using MongoDB.Driver;

namespace Infrastructure.Storage.RecordSheets;

public class RecordSheetItemStorage(AppDbContext context, IRequestContext requestContext, IStorage<User> userStorage)
    : StorageBase<RecordSheetItem, RecordSheetItemDbo>(context, requestContext)
{
    private readonly AppDbContext _context = context;
    protected override IMongoCollection<RecordSheetItemDbo> Collection => _context.RecordSheetItems;

    protected override async Task MapEntityFromDboAsync(RecordSheetItem entity, RecordSheetItemDbo dbo)
    {
        var specialist = await userStorage.GetByIdAsync(dbo.SpecialistId);
        var representative = await userStorage.GetByIdAsync(dbo.RepresentativeId);
        var complianceNoteUser = await userStorage.GetByIdAsync(dbo.ComplianceNoteUserId);
        entity.Id = dbo.Id;
        entity.Date = dbo.Date;
        entity.Deviations = dbo.Deviations;
        entity.Directions = dbo.Directions;
        entity.SpecialistSignature = specialist!.GetSignature();
        entity.RepresentativeSignature = representative!.GetSignature();
        entity.ComplianceNoteSignature = complianceNoteUser!.GetSignature();
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
        dbo.Directions = entity.Directions;
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
        dbo.Directions = newEntity.Directions;
        dbo.SpecialistId = newEntity.SpecialistId;
        dbo.RepresentativeId = newEntity.RepresentativeId;
        dbo.ComplianceNoteUserId = newEntity.ComplianceNoteUserId;
        dbo.RecordSheetId = newEntity.RecordSheetId;
        
        return Task.CompletedTask;
    }
}