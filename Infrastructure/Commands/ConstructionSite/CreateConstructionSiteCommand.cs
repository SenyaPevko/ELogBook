using Domain.Dtos;
using Domain.Entities.ConstructionSite;
using Domain.Repository;
using Domain.RequestArgs.CreationArgs;
using Infrastructure.Commands.Base;

namespace Infrastructure.Commands.ConstructionSite;

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

    protected override Task<ConstructionSiteDto>
        MapToDtoAsync(Domain.Entities.ConstructionSite.ConstructionSite entity)
    {
        return Task.FromResult(new ConstructionSiteDto
        {
            Name = entity.Name,
            Description = entity.Description,
            Address = entity.Address,
            Image = entity.Image,

            // todo: преобразование внутренних entity в dto
            // можно завести helper со всеми entity в dto, и переиспользовать его в MapToDto
            RegistrationSheet = default,
            RecordSheet = default,
            Orders = entity.Orders,
            ConstructionSiteUserRoleIds = default
        });
    }
}