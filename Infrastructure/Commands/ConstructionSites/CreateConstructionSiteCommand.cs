using Domain.Dtos;
using Domain.Entities.ConstructionSite;
using Domain.Repository;
using Domain.RequestArgs.CreationArgs;
using Infrastructure.Commands.Base;

namespace Infrastructure.Commands.ConstructionSites;

public class CreateConstructionSiteCommand(
    IRepository<Domain.Entities.ConstructionSite.ConstructionSite, InvalidConstructionSiteReason> repository)
    : CreateCommandBase<ConstructionSiteDto, Domain.Entities.ConstructionSite.ConstructionSite,
        ConstructionSiteCreationArgs, InvalidConstructionSiteReason>(repository)
{
    protected override Task<Domain.Entities.ConstructionSite.ConstructionSite> MapToEntityAsync(
        ConstructionSiteCreationArgs args)
    {
        return Task.FromResult(new Domain.Entities.ConstructionSite.ConstructionSite
        {
            Name = args.Name,
            Description = args.Description,
            Address = args.Address,
            Image = args.Image
        });
    }

    protected override async Task<ConstructionSiteDto> MapToDtoAsync(
        Domain.Entities.ConstructionSite.ConstructionSite entity) => await entity.ToDto();
}