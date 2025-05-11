using System.Net;
using Domain.Entities.Base;
using Domain.Models.ErrorInfo;

namespace Infrastructure.Helpers;

public static class ErrorInfoExtensions
{
    public static ErrorInfo ReadAccessForbidden<TEntity>() =>
        new($"Could not get {typeof(TEntity).Name}", "You do not have access", HttpStatusCode.Forbidden);

    public static CreateErrorInfo<TInvalidReason> CreateAccessForbidden<TEntity, TInvalidReason>()
        where TInvalidReason : Enum
        => new($"Could not create {typeof(TEntity).Name}", "You do not have access", HttpStatusCode.Forbidden);

    public static UpdateErrorInfo<TInvalidReason> UpdateAccessForbidden<TEntity, TInvalidReason>(Guid id)
        where TInvalidReason : Enum
        => new(
            $"Could not update {typeof(TEntity).Name}", 
            $"You do not have access to {typeof(TEntity).Name} with id {id}", 
            HttpStatusCode.Forbidden);
}