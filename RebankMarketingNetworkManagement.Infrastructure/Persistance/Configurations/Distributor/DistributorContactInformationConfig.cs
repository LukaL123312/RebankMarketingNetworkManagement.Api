using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RebankMarketingNetworkManagement.Domain;
using RebankMarketingNetworkManagement.Infrastructure.Persistance.Configurations.Common;

namespace RebankMarketingNetworkManagement.Infrastructure.Persistance.Configurations.Distributor;

internal class DistributorContactInformationConfig
    : EntityBaseConfig<DistributorContactInformation>
{
    public override void Configure(
        EntityTypeBuilder<DistributorContactInformation> builder)
    {
        base.Configure(builder);

        builder
            .HasKey(x => x.DistributorContactInformationID);

        builder
            .Property(x => x.DistributorContactInformationID)
            .ValueGeneratedOnAdd();

        builder
            .Property(x => x.ContactType)
            .IsRequired();

        builder
            .Property(x => x.ContactInformation)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .HasOne(d => d.Distributor)
            .WithOne(a => a.ContactInformation)
            .HasForeignKey<DistributorContactInformation>(a => a.DistributorID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}