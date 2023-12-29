using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RebankMarketingNetworkManagement.Infrastructure.Persistance.Configurations.Common;

namespace RebankMarketingNetworkManagement.Infrastructure.Persistance.Configurations.Distributor;

internal class DistributorBonusConfig
    : AuditableEntityConfig<Domain.DistributorBonus>
{
    public override void Configure(
        EntityTypeBuilder<Domain.DistributorBonus> builder)
    {
        base.Configure(builder);

        builder
            .HasKey(x => x.DistributorBonusID);

        builder
            .Property(x => x.DistributorBonusID)
            .ValueGeneratedOnAdd();

        builder
            .Property(x => x.DistributorID)
            .IsRequired();

        builder
            .Property(x => x.DistributorName)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(x => x.DistributorSurname)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .HasOne(d => d.Distributor)
            .WithMany(d => d.DistributorBonus)
            .HasForeignKey(d => d.DistributorID)
            .OnDelete(DeleteBehavior.Restrict);
    }
}