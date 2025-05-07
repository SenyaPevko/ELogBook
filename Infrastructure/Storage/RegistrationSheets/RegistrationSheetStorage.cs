using System.Data.Common;
using Domain.Entities.RegistrationSheet;
using Infrastructure.Context;
using Infrastructure.Dbo.ConstructionSite;
using Infrastructure.Dbo.RegistrationSheets;
using Infrastructure.Storage.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Storage.RegistrationSheets;

/*public class RegistrationSheetStorage(AppDbContext context, IRequestContext requestContext)
    : StorageBase<RegistrationSheet, RegistrationSheetDbo>(context, requestContext)
{
    public override async Task<RegistrationSheet?> GetByIdAsync(Guid id)
    {

    }

    protected override Task MapEntityFromDboAsync(RegistrationSheet entity, RegistrationSheetDbo dbo)
    {
        entity.Id = dbo.Id;
        // todo: маппиг в entity сторонних сущностей?
        entity.Items = default;
        
        return Task.CompletedTask;
    }

    protected override Task MapDboFromEntityAsync(RegistrationSheet entity, RegistrationSheetDbo dbo)
    {
        throw new NotImplementedException();
    }

    protected override Task MapDboFromEntityAsync(RegistrationSheet? existingEntity, RegistrationSheet newEntity,
        RegistrationSheetDbo dbo)
    {
        throw new NotImplementedException();
    }
}*/