using Domain.AccessChecker;
using Domain.Entities.ConstructionSite;
using Domain.Entities.RecordSheet;
using Domain.Entities.Roles;
using Domain.Repository;
using Domain.RequestArgs.ConstructionSites;
using Domain.RequestArgs.RecordSheetItems;
using Infrastructure.Context;

namespace Infrastructure.AccessCheckers.RecordSheets;

public class RecordSheetItemAccessChecker(
    IRequestContext context,
    IRepository<ConstructionSite, InvalidConstructionSiteReason, ConstructionSiteSearchRequest> constructionSiteStorage)
    : AccessCheckerBase<RecordSheetItem, RecordSheetItemUpdateArgs>(context), IRecordSheetItemAccessChecker
{
    public override async Task<bool> CanCreate(RecordSheetItem entity)
    {
        var userRoles = await GetUserRoleTypes(entity);

        return CanCreate(userRoles);
    }

    public override async Task<bool> CanUpdate(RecordSheetItem entity)
    {
        var userRoles = await GetUserRoleTypes(entity);

        return CanUpdate(userRoles);
    }

    public bool CanCreate(List<ConstructionSiteUserRoleType> userRoles)
    {
        return userRoles.Any(x =>x is ConstructionSiteUserRoleType.AuthorSupervision or ConstructionSiteUserRoleType.Admin);
    }
    
    public bool CanRead(List<ConstructionSiteUserRoleType> userRoles)
    {
        return true;
    }
    
    public bool CanUpdate(List<ConstructionSiteUserRoleType> userRoles)
    {
        return userRoles.Any(r =>
            r is ConstructionSiteUserRoleType.AuthorSupervision or ConstructionSiteUserRoleType.Customer
                or ConstructionSiteUserRoleType.Operator or ConstructionSiteUserRoleType.Admin);
    }
    
    public bool CanUpdate(RecordSheetItemUpdateArgs updateArgs, List<ConstructionSiteUserRoleType> userRoles)
    {
        if (userRoles.Contains(ConstructionSiteUserRoleType.Admin))
            return true;
        
        var canUpdateDeviations = updateArgs.Deviations is null || CanUpdateDeviations(userRoles);
        var canUpdateDirections = updateArgs.Directions is null || CanUpdateDirections(userRoles);
        var canUpdateRepresentativeId = updateArgs.RepresentativeId is null || CanUpdateRepresentativeId(userRoles);
        var canUpdateComplianceNoteUserId =
            updateArgs.ComplianceNoteUserId is null || CanUpdateComplianceNoteUserId(userRoles);

        return canUpdateDeviations && canUpdateDirections && canUpdateRepresentativeId && canUpdateComplianceNoteUserId;
    }

    public override async Task<bool> CanUpdate(RecordSheetItemUpdateArgs updateArgs, RecordSheetItem oldEntity,
        RecordSheetItem newEntity)
    {
        var userRoles = await GetUserRoleTypes(oldEntity);

        return CanUpdate(updateArgs, userRoles);
    }

    public async Task<List<ConstructionSiteUserRoleType>> GetUserRoleTypes(Guid constructionSiteId)
    {
        // todo: небезопасный First, хотя логичный - нужно переписывать логику валидации зависимостей и связанности, чтобы такой фигни не было
        var constructionSite = await constructionSiteStorage.GetByIdAsync(constructionSiteId, default);

        return constructionSite is null ? [] : constructionSite.GetUserRoleTypes(Context);
    }
    
    public async Task<List<ConstructionSiteUserRoleType>> GetUserRoleTypes(RecordSheetItem entity)
    {
        // todo: небезопасный First, хотя логичный - нужно переписывать логику валидации зависимостей и связанности, чтобы такой фигни не было
        var constructionSite = (await constructionSiteStorage.SearchAsync(new ConstructionSiteSearchRequest
            { RecordSheetId = entity.RecordSheetId }, default)).First();

        return constructionSite.GetUserRoleTypes(Context);
    }

    public bool CanUpdateDeviations(List<ConstructionSiteUserRoleType> userRoles)
    {
        return userRoles.Contains(ConstructionSiteUserRoleType.AuthorSupervision);
    }

    public bool CanUpdateDirections(List<ConstructionSiteUserRoleType> userRoles)
    {
        return userRoles.Contains(ConstructionSiteUserRoleType.AuthorSupervision);
    }

    public bool CanUpdateRepresentativeId(List<ConstructionSiteUserRoleType> userRoles)
    {
        return userRoles.Any(r => r is ConstructionSiteUserRoleType.Customer or ConstructionSiteUserRoleType.Operator);
    }

    public bool CanUpdateComplianceNoteUserId(List<ConstructionSiteUserRoleType> userRoles)
    {
        return userRoles.Any(r => r is ConstructionSiteUserRoleType.Customer or ConstructionSiteUserRoleType.Operator);
    }
}