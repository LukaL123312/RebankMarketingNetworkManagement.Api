using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using RebankMarketingNetworkManagement.Application.Common.Interfaces.Helpers;
using RebankMarketingNetworkManagement.Domain.Common;

namespace RebankMarketingNetworkManagement.Infrastructure.Persistance.Interceptors;

public class AuditableEntitySaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly ICurrentUser _currentUser;

    public AuditableEntitySaveChangesInterceptor(
        ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(eventData, nameof(eventData));

        UpdateEntities(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        ArgumentNullException.ThrowIfNull(eventData, nameof(eventData));

        UpdateEntities(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    private void UpdateEntities(DbContext context)
    {
        if (context == null)
            return;

        foreach (var entry in context.ChangeTracker.Entries<AuditableEntity>())
        {
            if (entry.State == EntityState.Added &&
                entry.Metadata.FindProperty(nameof(AuditableEntity.CreatedById)) is not null)
                entry.Entity.CreatedById = _currentUser.Id;

            if (entry.State == EntityState.Modified
                || HasChangedOwnedEntities(entry))
            {
                entry.Entity.UpdatedById = _currentUser.Id;
                entry.Entity.UpdatedDateTime = DateTime.UtcNow;
            }
        }
    }

    private static bool HasChangedOwnedEntities(EntityEntry entry)
    {
        ArgumentNullException.ThrowIfNull(entry, nameof(entry));

        return entry.References.Any(r =>
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned() &&
            r.TargetEntry.State is EntityState.Added or EntityState.Modified);
    }
}

