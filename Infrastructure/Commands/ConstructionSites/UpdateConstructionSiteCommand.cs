using Domain.Dtos;
using Domain.Entities.ConstructionSite;
using Domain.Repository;
using Domain.RequestArgs.ConstructionSites;
using Infrastructure.Commands.Base;

namespace Infrastructure.Commands.ConstructionSites;

public class UpdateConstructionSiteCommand(
    IRepository<ConstructionSite, InvalidConstructionSiteReason> repository)
    : UpdateCommandBase<ConstructionSiteDto, ConstructionSite,
        ConstructionSiteUpdateArgs, InvalidConstructionSiteReason>(repository)
{
    protected override async Task<ConstructionSiteDto> MapToDtoAsync(ConstructionSite entity)
    {
        return await entity.ToDto();
    }

    protected override Task ApplyUpdatesAsync(ConstructionSite entity,
        ConstructionSiteUpdateArgs args)
    {
        if (args.Name is not null) entity.Name = args.Name;
        if (args.Description is not null) entity.Description = args.Description;
        if (args.Address is not null) entity.Address = args.Address;
        if (args.Image is not null) entity.Image = args.Image;

        return Task.FromResult(entity);
    }
}