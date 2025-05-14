using Core.Helpers;
using Domain.Dtos;
using Domain.Dtos.ConstructionSite;
using Domain.Dtos.RecordSheet;
using Domain.Dtos.RegistrationSheet;
using Domain.Dtos.WorkIssue;
using Domain.Entities.ConstructionSite;
using Domain.Entities.Organization;
using Domain.Entities.RecordSheet;
using Domain.Entities.RegistrationSheet;
using Domain.Entities.Users;
using Domain.Entities.WorkIssues;
using Domain.FileStorage;
using Domain.Repository;

namespace Infrastructure.Commands;

public static class DtoHelper
{
    public static async Task<ConstructionSiteDto> ToDto(
        this ConstructionSite entity,
        IFileStorageService fileStorageService,
        IRepository<Organization> organizationRepository)
    {
        return new ConstructionSiteDto
        {
            Id = entity.Id,
            UpdateInfo = entity.UpdateInfo,
            ShortName = entity.ShortName,
            FullName = entity.FullName,
            Address = entity.Address,
            Orders = (await entity.Orders.SelectAsync(o => o.ToDto(fileStorageService))).ToList(),
            ConstructionSiteUserRoles = entity.ConstructionSiteUserRoles,
            Organization = await (await organizationRepository.GetByIdAsync(entity.OrganizationId, default)).ToDto(),
            SubOrganization = await (await organizationRepository.GetByIdAsync(entity.SubOrganizationId, default)).ToDto(),
            
            RegistrationSheet = await entity.RegistrationSheet.ToDto(),
            RecordSheet = await entity.RecordSheet.ToDto(fileStorageService),
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

    public static async Task<RecordSheetDto> ToDto(this RecordSheet entity, IFileStorageService fileStorageService)
    {
        return new RecordSheetDto
        {
            Id = entity.Id,
            UpdateInfo = entity.UpdateInfo,
            Number = entity.Number,
            Items = (await entity.Items.SelectAsync(item => item.ToDto(fileStorageService))).ToList(),
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

    public static async Task<RecordSheetItemDto> ToDto(this RecordSheetItem entity, IFileStorageService fileStorage) =>
        new()
        {
            Id = entity.Id,
            UpdateInfo = entity.UpdateInfo,
            Date = entity.Date,
            Deviations = entity.Deviations,
            Directions = entity.Directions,
            SpecialistSignature = entity.SpecialistSignature,
            ComplianceNote = entity.ComplianceNoteSignature,
            RepresentativeSignature = entity.RepresentativeSignature,
            
            // todo: надо с null что то делать
            DeviationFiles = (await entity.DeviationFilesIds?.SelectAsync(fileStorage.GetFileInfoAsync)).ToList(),
            DirectionFiles = (await entity.DirectionFilesIds?.SelectAsync(fileStorage.GetFileInfoAsync)).ToList(),
        };

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

    public static async Task<OrderDto> ToDto(this Order entity, IFileStorageService storageService) =>
        new()
        {
            Id = entity.Id,
            UserInChargeId = entity.UserInChargeId,
            File = await storageService.GetFileInfoAsync(entity.FileId),
        };
}