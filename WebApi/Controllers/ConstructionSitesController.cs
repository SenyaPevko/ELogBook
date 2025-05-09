using Domain.Commands;
using Domain.Dtos;
using Domain.Entities;
using Domain.Entities.ConstructionSite;
using Domain.Models.ErrorInfo;
using Domain.RequestArgs.ConstructionSites;
using Domain.RequestArgs.SearchRequest;
using ELogBook.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using ConstructionSite = Domain.Entities.ConstructionSite.ConstructionSite;

namespace ELogBook.Controllers;

public class ConstructionSitesController
    : CreatableEntityControllerBase<ConstructionSiteDto, ConstructionSite, ConstructionSiteCreationArgs,
        ConstructionSiteUpdateArgs,
        InvalidConstructionSiteReason, ConstructionSiteSearchRequest>
{
}