using Core.Helpers;
using Domain.Dtos;
using Domain.Dtos.RegistrationSheet;
using Domain.Entities.ConstructionSite;
using Domain.Entities.Organization;
using Domain.Entities.RegistrationSheet;
using Domain.Entities.Roles;
using Domain.Entities.Users;

namespace Infrastructure.Commands;

public static class DtoHelper
{
    public static Task<ConstructionSiteDto> ToDto(this ConstructionSite entity) =>
        Task.FromResult(new ConstructionSiteDto
        {
            Id = entity.Id,
            UpdateInfo = entity.UpdateInfo,
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

    public static Task<UserDto> ToDto(this User entity) =>
        Task.FromResult(new UserDto
        {
            Id = entity.Id,
            UpdateInfo = entity.UpdateInfo,
            Name = entity.Name,
            Surname = entity.Surname,
            Patronymic = entity.Patronymic,
            Email = entity.Email,
            OrganizationName = entity.OrganizationName,
            OrganizationId = entity.OrganizationId,
            UserRole = entity.UserRole
        });

    public static Task<OrganizationDto> ToDto(this Organization entity) =>
        Task.FromResult(new OrganizationDto
        {
            Id = entity.Id,
            UpdateInfo = entity.UpdateInfo,
            Name = entity.Name,
            UserIds = entity.UserIds
        });
    
    public static Task<RegistrationSheetItemDto> ToDto(this RegistrationSheetItem entity) =>
        Task.FromResult(new RegistrationSheetItemDto
        {
            Id = entity.Id,
            UpdateInfo = entity.UpdateInfo,
            OrganizationName = entity.OrganizationName,
            Name = entity.Name,
            Surname = entity.Surname,
            Patronymic = entity.Patronymic,
            Signature = entity.Signature,
            ArrivalDate = entity.ArrivalDate,
            DepartureDate = entity.DepartureDate,
        });
}