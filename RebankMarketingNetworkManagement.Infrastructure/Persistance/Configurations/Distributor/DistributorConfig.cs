using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RebankMarketingNetworkManagement.Infrastructure.Persistance.Configurations.Common;

namespace RebankMarketingNetworkManagement.Infrastructure.Persistance.Configurations.Distributor;

internal class DistributorConfig
    : AuditableEntityConfig<Domain.Distributor>
{
    public override void Configure(
        EntityTypeBuilder<Domain.Distributor> builder)
    {
        base.Configure(builder);

        builder
            .HasKey(x => x.DistributorID);

        builder
            .Property(x => x.DistributorID)
            .ValueGeneratedOnAdd();

        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(x => x.Surname)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(x => x.BirthDate)
            .IsRequired();

        builder
            .Property(x => x.Gender)
            .IsRequired();

        builder
            .HasOne(d => d.Recommender)
            .WithMany(d => d.RecommendedDistributors)
            .HasForeignKey(d => d.RecommenderID)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(x => x.DistributorSales)
            .WithOne(x => x.Distributor)
            .HasForeignKey(x => x.DistributorID);
    }
}