using Domain.Entities.Base;
using Domain.Models.ErrorInfo;
using Domain.Repository;

namespace Infrastructure.WriteContext;

public class BulkWriteContext<TEntity, TInvalidReason> : IBulkWriteContext<TEntity, TInvalidReason>
    where TInvalidReason : Enum
    where TEntity : EntityInfo
{
    public Dictionary<string, List<ErrorDetail<TInvalidReason>>> Errors { get; } = [];
    public bool IsSuccess => Errors.Count == 0;

    public void AddInvalidData(TEntity entity, List<ErrorDetail<TInvalidReason>> invalidData)
    {
        // todo: по id не стоит делать, тк при создании этот параметр неизвестен на фронте, и определить объект ошибки невозможно
        // нужен некий errorKey для каждой сущности, который принимался бы с фронта
        if (Errors.TryGetValue(entity.Id.ToString(), out var data))
            data.AddRange(invalidData);
        else
            Errors.Add(entity.Id.ToString(), invalidData);
    }

    public void AddInvalidData(TEntity entity, ErrorDetail<TInvalidReason> invalidData)
    {
        AddInvalidData(entity, [invalidData]);
    }
}