using Domain.Commands;
using Domain.Dtos.Notifications;
using Domain.Entities.Notifications;
using Domain.Models.ErrorInfo;
using Domain.Models.Result;
using Domain.RequestArgs.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ELogBook.Controllers;

[ApiController]
[Authorize]
[Route("api/" + "[controller]")]
public class RecordSheetItemNotificationsController : ControllerBase
{
    /// <summary>
    ///     Поиск
    /// </summary>
    /// <param name="command"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<List<RecordSheetItemNotificationDto>, ErrorInfo>> Search(
        [FromServices] ISearchCommand<RecordSheetItemNotificationDto, NotificationSearchRequest> command,
        [FromQuery] NotificationSearchRequest request)
    {
        return await command.ExecuteAsync(request, HttpContext.RequestAborted);
    }

    /// <summary>
    ///     Обновить
    /// </summary>
    /// <param name="command"></param>
    /// <param name="request"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPatch("{id:guid}")]
    public async Task<ActionResult<RecordSheetItemNotificationDto, UpdateErrorInfo<InvalidNotificationReason>>> Update(
        [FromServices]
        IUpdateCommand<RecordSheetItemNotificationDto, NotificationUpdateArgs, InvalidNotificationReason> command,
        [FromBody] NotificationUpdateArgs request,
        [FromRoute] Guid id)
    {
        request.Id = id;

        return await command.ExecuteAsync(request, HttpContext.RequestAborted);
    }
}