using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RebankMarketingNetworkManagement.Domain;
using RebankMarketingNetworkManagement.Infrastructure.Persistance.Configurations.Common;

namespace RebankMarketingNetworkManagement.Infrastructure.Persistance.Configurations.Distributor;

internal class DistributorPrivateDocumentInformationConfig
    : EntityBaseConfig<DistributorPrivateDocumentInformation>
{
    public override void Configure(
        EntityTypeBuilder<DistributorPrivateDocumentInformation> builder)
    {
        base.Configure(builder);

        builder
            .HasKey(x => x.DistributorPrivateDocumentInformationID);

        builder
            .Property(x => x.DistributorPrivateDocumentInformationID)
            .ValueGeneratedOnAdd();

        builder
            .Property(x => x.DocumentType)
            .IsRequired();

        builder
            .Property(x => x.SerialNumber)
            .HasMaxLength(10);

        builder
            .Property(x => x.Number)
            .HasMaxLength(10);

        builder
            .Property(x => x.IssuingDate)
            .IsRequired();

        builder
            .Property(x => x.ExpiryDate)
            .IsRequired();

        builder
            .Property(x => x.PrivateNumber)
            .HasMaxLength(50)
            .IsRequired();

        builder
            .Property(x => x.IssuerOrganization)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .HasOne(d => d.Distributor)
            .WithOne(a => a.PrivateDocumentInformation)
            .HasForeignKey<DistributorPrivateDocumentInformation>(a => a.DistributorID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}