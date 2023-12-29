using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RebankMarketingNetworkManagement.Infrastructure.Persistance.Configurations.Common;

namespace RebankMarketingNetworkManagement.Infrastructure.Persistance.Configurations.Product;

internal class ProductConfig
    : AuditableEntityConfig<Domain.Product>
{
    public override void Configure(
        EntityTypeBuilder<Domain.Product> builder)
    {
        base.Configure(builder);

        builder
            .HasKey(x => x.ProductID);

        builder
            .Property(x => x.ProductID)
            .ValueGeneratedOnAdd();

        builder
            .Property(x => x.ProductCode)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(x => x.ProductName)
            .IsRequired()
            .HasMaxLength(250);

        builder
            .Property(x => x.UnitPrice)
            .IsRequired();

        builder
            .HasMany(x => x.DistributorSales)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductID);
    }
}