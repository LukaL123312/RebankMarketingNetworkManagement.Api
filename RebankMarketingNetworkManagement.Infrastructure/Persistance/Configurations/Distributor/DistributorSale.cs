using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RebankMarketingNetworkManagement.Infrastructure.Persistance.Configurations.Common;

namespace RebankMarketingNetworkManagement.Infrastructure.Persistance.Configurations.Distributor;

internal class DistributorSale
    : AuditableEntityConfig<Domain.DistributorSale>
{
    public override void Configure(
        EntityTypeBuilder<Domain.DistributorSale> builder)
    {
        base.Configure(builder);

        builder
            .HasKey(x => x.DistributorSaleID);

        builder
            .Property(x => x.DistributorSaleID)
            .ValueGeneratedOnAdd();

        builder
            .Property(x => x.DistributorID)
            .IsRequired();

        builder
            .Property(x => x.SaleDate)
            .IsRequired();

        builder
            .Property(x => x.ProductID)
            .IsRequired();

        builder
            .Property(x => x.UnitPrice)
            .IsRequired();

        builder
            .Property(x => x.Quantity)
            .IsRequired();

        builder
            .Property(x => x.SumSalePrice)
            .IsRequired();

        builder
            .HasOne(d => d.Distributor)
            .WithMany(d => d.DistributorSales)
            .HasForeignKey(d => d.DistributorID)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(d => d.Product)
            .WithMany(d => d.DistributorSales)
            .HasForeignKey(d => d.ProductID)
            .OnDelete(DeleteBehavior.Restrict);
    }
}