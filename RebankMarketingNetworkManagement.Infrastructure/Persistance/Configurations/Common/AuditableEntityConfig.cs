using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RebankMarketingNetworkManagement.Domain.Common;

namespace RebankMarketingNetworkManagement.Infrastructure.Persistance.Configurations.Common;

internal abstract class AuditableEntityConfig<TEntity>
    : EntityBaseConfig<TEntity>
    where TEntity : AuditableEntity
{
    public override void Configure(
        EntityTypeBuilder<TEntity> builder)
    {
        base.Configure(builder);

        builder
            .Property(x => x.CreatedById)
            .IsRequired();

        builder
            .Property(x => x.CreatedDateTime)
            .HasDefaultValueSql("GETUTCDATE()")
            .ValueGeneratedOnAdd();
    }
}

