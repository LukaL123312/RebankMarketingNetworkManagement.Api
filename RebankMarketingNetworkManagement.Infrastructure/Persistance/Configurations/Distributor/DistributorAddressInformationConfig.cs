using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RebankMarketingNetworkManagement.Domain;
using RebankMarketingNetworkManagement.Infrastructure.Persistance.Configurations.Common;

namespace RebankMarketingNetworkManagement.Infrastructure.Persistance.Configurations.Distributor;

internal class DistributorAddressInformationConfig
    : EntityBaseConfig<DistributorAddressInformation>
{
    public override void Configure(
        EntityTypeBuilder<DistributorAddressInformation> builder)
    {
        base.Configure(builder);

        builder
            .HasKey(x => x.DistributorAddressInformationID);

        builder
            .Property(x => x.DistributorAddressInformationID)
            .ValueGeneratedOnAdd();

        builder
            .Property(x => x.AddressType)
            .IsRequired();

        builder
            .Property(x => x.Address)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .HasOne(d => d.Distributor)
            .WithOne(a => a.AddressInformation)
            .HasForeignKey<DistributorAddressInformation>(a => a.DistributorID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}