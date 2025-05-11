using Domain.AccessChecker;
using Domain.Dtos.RegistrationSheet;
using Domain.Entities.RegistrationSheet;
using Domain.Repository;
using Domain.RequestArgs.RegistrationSheetItems;
using Infrastructure.Commands.Base;
using Infrastructure.Context;

namespace Infrastructure.Commands.RegistrationSheetItems;

public class CreateRegistrationSheetItemCommand(
    IRepository<RegistrationSheetItem, InvalidRegistrationSheetItemReason> repository,
    IRequestContext context,
    IAccessChecker<RegistrationSheetItem> accessChecker)
    : CreateCommandBase<RegistrationSheetItemDto, RegistrationSheetItem, RegistrationSheetItemCreationArgs,
        InvalidRegistrationSheetItemReason>(repository, accessChecker)
{
    protected override async Task<RegistrationSheetItemDto> MapToDtoAsync(RegistrationSheetItem entity)
    {
        return await entity.ToDto();
    }

    protected override Task<RegistrationSheetItem> MapToEntityAsync(RegistrationSheetItemCreationArgs args)
    {
        return Task.FromResult(new RegistrationSheetItem
        {
            Id = args.Id,
            ArrivalDate = args.ArrivalDate,
            DepartureDate = args.DepartureDate,
            CreatorId = context.Auth.UserId!.Value,
            RegistrationSheetId = args.RegistrationSheetId
        });
    }
}