using Domain;
using Domain.Models.ErrorInfo;
using Domain.Repository;

namespace Infrastructure.WriteContext;

public class WriteContext<TInvalidReason> : IWriteContext<TInvalidReason>
    where TInvalidReason : Enum
{
    public List<ErrorDetail<TInvalidReason>> Errors { get; } = [];

    public bool IsSuccess => Errors.Count == 0;

    public void AddInvalidData(ErrorDetail<TInvalidReason> invalidData)
    {
        Errors.Add(invalidData);
    }
}