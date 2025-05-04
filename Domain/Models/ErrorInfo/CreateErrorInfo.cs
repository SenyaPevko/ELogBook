using System.Net;

namespace Domain.Models.ErrorInfo;

public class CreateErrorInfo<TInvalidReason>(string title, string message, HttpStatusCode code)
    : ErrorInfo(title, message, code)
    where TInvalidReason : Enum
{
    public List<ErrorDetail<TInvalidReason>> Errors { get; set; } = [];
}