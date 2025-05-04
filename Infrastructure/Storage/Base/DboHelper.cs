using Domain.Entities.Base;
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


    public static TDbo CreateEntityDbo<TDbo>()
        where TDbo : EntityDbo, new()
    {
        return new TDbo();
    }
}