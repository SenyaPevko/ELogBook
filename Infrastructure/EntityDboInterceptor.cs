using Infrastructure.Context;
using Infrastructure.Dbo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure;

public class EntityDboInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var entries = eventData.Context?.ChangeTracker.Entries<EntityDbo>();

        if (entries != null)
        {
            var context = RequestContextHolder.Current;
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = context.RequestTime;
                    entry.Entity.CreatedByUserId = context.Auth.UserId;
                }

                if (entry.State is EntityState.Modified or EntityState.Added)
                {
                    entry.Entity.UpdatedAt = context.RequestTime;
                    entry.Entity.UpdatedByUserId = context.Auth.UserId;
                }
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}