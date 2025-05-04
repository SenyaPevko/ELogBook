namespace Domain.Models.ErrorInfo;

public class ErrorDetail<TInvalidReason> where TInvalidReason : Enum
{
    public required string Path { get; set; }

    public required TInvalidReason Reason { get; set; }

    public required string Value { get; set; }
}