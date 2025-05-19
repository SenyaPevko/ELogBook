using Domain.Entities.Base;
using Domain.Models.ErrorInfo;

namespace Domain.Repository;

public interface IBulkWriteContext<in TEntity, TInvalidReason>
    where TInvalidReason : Enum
    where TEntity : EntityInfo
{
    public Dictionary<string, List<ErrorDetail<TInvalidReason>>> Errors { get; }

    public bool IsSuccess { get; }

    public void AddInvalidData(TEntity entity, List<ErrorDetail<TInvalidReason>> invalidData); 
    
    public void AddInvalidData(TEntity entity, ErrorDetail<TInvalidReason> invalidData); 
}