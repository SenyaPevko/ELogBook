using Core.Helpers;
using Domain.Dtos;
using Domain.Dtos.RecordSheet;
using Domain.Dtos.RegistrationSheet;
using Domain.Dtos.WorkIssue;
using Domain.Entities.ConstructionSite;
using Domain.Entities.Organization;
using Domain.Entities.RecordSheet;
using Domain.Entities.RegistrationSheet;
using Domain.Entities.Roles;
using Domain.Entities.Users;
using Domain.Entities.WorkIssues;

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
    
    public static Task<RecordSheetItemDto> ToDto(this RecordSheetItem entity) =>
        Task.FromResult(new RecordSheetItemDto
        {
            Id = entity.Id,
            UpdateInfo = entity.UpdateInfo,
            Date = entity.Date,
            Deviations = entity.Deviations,
            Directions = entity.Directions,
            SpecialistSignature = entity.SpecialistSignature,
            ComplianceNote = entity.ComplianceNoteSignature,
            RepresentativeSignature = entity.RepresentativeSignature
        });
    
    public static Task<WorkIssueItemDto> ToDto(this WorkIssueItem entity) =>
        Task.FromResult(new WorkIssueItemDto
        {
            Id = entity.Id,
            UpdateInfo = entity.UpdateInfo,
            Answer = entity.Answer,
            AnswerDate = entity.AnswerDate,
            Question = entity.Question,
            QuestionDate = entity.QuestionDate,
        });
}