using Domain.Dtos;
using Domain.Entities.Organization;
using Domain.RequestArgs.Organizations;
using ELogBook.Controllers.Base;

namespace ELogBook.Controllers;

public class OrganizationController
    : CreatableEntityControllerBase<OrganizationDto, Organization, OrganizationCreationArgs, OrganizationUpdateArgs,
        InvalidOrganizationReason>
{
}