using Domain.Dtos;
using Domain.Entities.ConstructionSite;
using Domain.RequestArgs.ConstructionSites;
using Domain.RequestArgs.SearchRequest;
using ELogBook.Controllers.Base;
using ConstructionSite = Domain.Entities.ConstructionSite.ConstructionSite;

namespace ELogBook.Controllers;

public class ConstructionSitesController
    : CreatableEntityControllerBase<ConstructionSiteDto, ConstructionSite, ConstructionSiteCreationArgs,
        ConstructionSiteUpdateArgs,
        InvalidConstructionSiteReason, ConstructionSiteSearchRequest>
{
}