using Domain.Models.ErrorInfo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Domain.Entities;

public class ActionResult<TValue, TError> : IConvertToActionResult
    where TError : ErrorInfo
{
    private ActionResult(TValue value)
    {
        Value = value;
    }

    private ActionResult(TError error)
    {
        Error = error;
    }

    private ActionResult(IActionResult result)
    {
        Result = result ?? throw new ArgumentNullException(nameof(result));
    }

    public TValue? Value { get; }

    public TError? Error { get; }

    private IActionResult? Result { get; }

    IActionResult IConvertToActionResult.Convert()
    {
        if (Result != null)
            return Result;

        if (Error != null)
            return new ConflictObjectResult(Error);

        return new ObjectResult(Value);
    }

    public static implicit operator ActionResult<TValue, TError>(TValue value)
    {
        return new ActionResult<TValue, TError>(value);
    }

    public static implicit operator ActionResult<TValue, TError>(TError error)
    {
        return new ActionResult<TValue, TError>(error);
    }

    public static implicit operator ActionResult<TValue, TError>(ActionResult result)
    {
        return new ActionResult<TValue, TError>(result);
    }
}