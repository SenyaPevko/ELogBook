using Core.Helpers;
using Domain.Dtos;
using Domain.Dtos.RecordSheet;
using Domain.Dtos.RegistrationSheet;
using Domain.Dtos.WorkIssue;
using Domain.Entities.ConstructionSite;
using Domain.Entities.Organization;
using Domain.Entities.RecordSheet;
using Domain.Entities.RegistrationSheet;
using Domain.Entities.Users;
using Domain.Entities.WorkIssues;

namespace Infrastructure.Commands;

public static class DtoHelper
{
    public static async Task<ConstructionSiteDto> ToDto(this ConstructionSite entity)
    {
        return new ConstructionSiteDto
        {
            Id = entity.Id,
            UpdateInfo = entity.UpdateInfo,
            Name = entity.Name,
            Description = entity.Description,
            Address = entity.Address,
            Image = entity.Image,
            Orders = entity.Orders,
            ConstructionSiteUserRoles = entity.ConstructionSiteUserRoles,

            RegistrationSheet = await entity.RegistrationSheet.ToDto(),
            RecordSheet = await entity.RecordSheet.ToDto(),
            WorkIssue = await entity.WorkIssue.ToDto()
        };
    }

    public static async Task<RegistrationSheetDto> ToDto(this RegistrationSheet entity)
    {
        return new RegistrationSheetDto
        {
            Id = entity.Id,
            UpdateInfo = entity.UpdateInfo,
            Items = (await entity.Items.SelectAsync(item => item.ToDto())).ToList(),
            ConstructionSiteId = entity.ConstructionSiteId
        };
    }

    public static async Task<RecordSheetDto> ToDto(this RecordSheet entity)
    {
        return new RecordSheetDto
        {
            Id = entity.Id,
            UpdateInfo = entity.UpdateInfo,
            Number = entity.Number,
            Items = (await entity.Items.SelectAsync(item => item.ToDto())).ToList(),
            ConstructionSiteId = entity.ConstructionSiteId
        };
    }

    public static async Task<WorkIssueDto> ToDto(this WorkIssue entity)
    {
        return new WorkIssueDto
        {
            Id = entity.Id,
            UpdateInfo = entity.UpdateInfo,
            Items = (await entity.Items.SelectAsync(item => item.ToDto())).ToList(),
            ConstructionSiteId = entity.ConstructionSiteId
        };
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

    public static Task<OrganizationDto> ToDto(this Organization entity)
    {
        return Task.FromResult(new OrganizationDto
        {
            Id = entity.Id,
            UpdateInfo = entity.UpdateInfo,
            Name = entity.Name,
            UserIds = entity.UserIds
        });
    }

    public static Task<RegistrationSheetItemDto> ToDto(this RegistrationSheetItem entity)
    {
        return Task.FromResult(new RegistrationSheetItemDto
        {
            Id = entity.Id,
            UpdateInfo = entity.UpdateInfo,
            OrganizationName = entity.OrganizationName,
            Name = entity.Name,
            Surname = entity.Surname,
            Patronymic = entity.Patronymic,
            Signature = entity.Signature,
            ArrivalDate = entity.ArrivalDate,
            DepartureDate = entity.DepartureDate
        });
    }

    public static Task<RecordSheetItemDto> ToDto(this RecordSheetItem entity)
    {
        return Task.FromResult(new RecordSheetItemDto
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
    }

    public static Task<WorkIssueItemDto> ToDto(this WorkIssueItem entity)
    {
        return Task.FromResult(new WorkIssueItemDto
        {
            Id = entity.Id,
            UpdateInfo = entity.UpdateInfo,
            Answer = entity.Answer,
            AnswerDate = entity.AnswerDate,
            Question = entity.Question,
            QuestionDate = entity.QuestionDate,
            AnswerUserId = entity.AnswerUserId,
        });
    }
}