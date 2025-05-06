using Domain.Dtos;
using Domain.Entities.ConstructionSite;
using Domain.Entities.Users;

namespace Infrastructure.Commands;

public static class DtoHelper
{
    public static Task<ConstructionSiteDto> ToDto(this ConstructionSite entity)
    {
        return Task.FromResult(new ConstructionSiteDto
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
    }

    public static Task<UserDto> ToDto(this User entity)
    {
        return Task.FromResult(new UserDto
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
    }
}