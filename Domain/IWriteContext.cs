using Domain.Models.ErrorInfo;

namespace Domain;

public interface IWriteContext<TInvalidReason>
    where TInvalidReason : Enum
{
    public List<ErrorDetail<TInvalidReason>> Errors { get; }

    public bool IsSuccess { get; }

    public void AddInvalidData(ErrorDetail<TInvalidReason> invalidData);
}