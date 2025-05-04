using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Models.ErrorInfo;

public class ErrorInfo : ProblemDetails
{
    public ErrorInfo(string title, string message, HttpStatusCode code)
    {
        Title = title;
        Status = (int)code;
        Detail = message;
    }
}