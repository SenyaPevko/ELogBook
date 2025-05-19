using Domain.Dtos.ConstructionSite;
using Domain.Entities.ConstructionSite;
using Domain.Models.ErrorInfo;
using Domain.Models.Result;
using Domain.RequestArgs.ConstructionSites;
using ELogBook.Controllers.Base;
using Infrastructure.Commands.ConstructionSites;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ConstructionSite = Domain.Entities.ConstructionSite.ConstructionSite;

namespace ELogBook.Controllers;

public class ConstructionSitesController
    : CreatableEntityControllerBase<ConstructionSiteDto, ConstructionSite, ConstructionSiteCreationArgs,
        ConstructionSiteUpdateArgs,
        InvalidConstructionSiteReason, ConstructionSiteSearchRequest>
{
}