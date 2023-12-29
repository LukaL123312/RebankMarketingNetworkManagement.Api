using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RebankMarketingNetworkManagement.Domain.Common;

namespace RebankMarketingNetworkManagement.Infrastructure.Persistance.Configurations.Common;

internal abstract class EntityBaseConfig<TEntity>
    : IEntityTypeConfiguration<TEntity>
    where TEntity : EntityBase
{
    public virtual void Configure(
        EntityTypeBuilder<TEntity> builder) =>
        builder
            .HasQueryFilter(e => !e.Deleted);
}
