using Domain.Dtos;
using Domain.Entities.ConstructionSite;
using Domain.RequestArgs.CreationArgs;
using Domain.RequestArgs.UpdateArgs;
using ELogBook.Controllers.Base;

namespace ELogBook.Controllers;

public class ConstructionSitesController
    : CreatableEntityControllerBase<ConstructionSiteDto, ConstructionSite, ConstructionSiteCreationArgs,
        ConstructionSiteUpdateArgs,
        InvalidConstructionSiteReason>
{
}