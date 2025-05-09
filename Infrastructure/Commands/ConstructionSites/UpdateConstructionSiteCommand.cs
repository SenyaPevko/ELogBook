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
    protected override async Task<ConstructionSiteDto> MapToDtoAsync(ConstructionSite entity) => await entity.ToDto();
    
    protected override Task ApplyUpdatesAsync(ConstructionSite entity,
        ConstructionSiteUpdateArgs args)
    {
        if (args.Name is not null) entity.Name = args.Name;
        if (args.Description is not null) entity.Description = args.Description;
        if (args.Address is not null) entity.Address = args.Address;
        if (args.Image is not null) entity.Image = args.Image;
        if (args.Orders is not null)
        {
            if(args.Orders.Add is not null)
                // todo: нужно id добавлять наверное
                entity.Orders.AddRange(args.Orders.Add);
            if(args.Orders.Remove is not null)
                // todo: нужно except по id делать
                // todo: мб в ListUpdate для Add сделать один тип, а для Remove просто id 
                entity.Orders = entity.Orders.Except(args.Orders.Remove).ToList();
        }
        if (args.UserRoles is not null)
        {
            if(args.UserRoles.Add is not null)
                entity.ConstructionSiteUserRoles.AddRange(args.UserRoles.Add);
            if(args.UserRoles.Remove is not null)
                entity.ConstructionSiteUserRoles = entity.ConstructionSiteUserRoles.Except(args.UserRoles.Remove).ToList();
        }

        return Task.FromResult(entity);
    }
}