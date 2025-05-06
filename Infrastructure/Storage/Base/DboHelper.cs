using Domain.Entities.Base;
using Infrastructure.Context;
using Infrastructure.Dbo;

namespace Infrastructure.Storage.Base;

public static class DboHelper
{
    public static UpdateInfo ToUpdateInfo<TDbo>(this TDbo dbo)
        where TDbo : EntityDbo
    {
        return new UpdateInfo
        {
            CreatedAt = dbo.CreatedAt.ToUniversalTime(),
            CreatedByUserId = dbo.CreatedByUserId,
            UpdatedAt = dbo.UpdatedAt.ToUniversalTime(),
            UpdatedByUserId = dbo.UpdatedByUserId
        };
    }


    public static TDbo CreateEntityDbo<TDbo>(IRequestContext requestContext)
        where TDbo : EntityDbo, new()
    {
        return new TDbo
        {
            CreatedAt = requestContext.RequestTime.ToUniversalTime(),
            CreatedByUserId = requestContext.Auth.UserId,
            UpdatedAt = requestContext.RequestTime.ToUniversalTime(),
            UpdatedByUserId = requestContext.Auth.UserId
        };
    }

    public static void UpdateEntityDbo<TDbo>(TDbo dbo, IRequestContext requestContext)
        where TDbo : EntityDbo, new()
    {
        dbo.UpdatedAt = requestContext.RequestTime.ToUniversalTime();
        dbo.UpdatedByUserId = requestContext.Auth.UserId;
    }
    
    public static void UpdateEntityInfo<TEntity, TDbo>(TEntity entity, TDbo dbo)
        where TEntity : EntityInfo, new()
        where TDbo : EntityDbo, new()
    {
        entity.UpdateInfo = new UpdateInfo
        {
            CreatedAt = dbo.CreatedAt,
            CreatedByUserId = dbo.CreatedByUserId,
            UpdatedAt = dbo.UpdatedAt,
            UpdatedByUserId = dbo.UpdatedByUserId
        };
    }
}