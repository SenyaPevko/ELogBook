namespace Domain.Models.ErrorInfo;

public class ErrorDetail<TInvalidReason> where TInvalidReason : Enum
{
    public string? Path { get; set; }

    public required TInvalidReason Reason { get; set; }

    public string? Value { get; set; }
}