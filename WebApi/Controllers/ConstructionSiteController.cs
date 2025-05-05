using Domain.Dtos;
using Domain.Entities.ConstructionSite;
using Domain.RequestArgs.CreationArgs;
using ELogBook.Controllers.Base;

namespace ELogBook.Controllers;

public class ConstructionSiteController
    : CreatableEntityControllerBase<ConstructionSiteDto, ConstructionSite, ConstructionSiteCreationArgs,
        InvalidConstructionSiteReason>
{
}