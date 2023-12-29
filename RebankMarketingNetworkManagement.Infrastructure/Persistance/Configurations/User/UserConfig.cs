using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RebankMarketingNetworkManagement.Infrastructure.Persistance.Configurations.Common;

namespace RebankMarketingNetworkManagement.Infrastructure.Persistance.Configurations.User;

internal class UserConfig
    : EntityBaseConfig<Domain.User>
{
    public override void Configure(
        EntityTypeBuilder<Domain.User> builder)
    {
        base.Configure(builder);

        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder
            .Property(x => x.Username)
            .HasMaxLength(70)
            .IsRequired();

        builder
            .Property(x => x.Password)
            .HasMaxLength(70)
            .IsRequired();

        builder
            .Property(x => x.Salt)
            .HasMaxLength(30)
            .IsRequired();

        builder
            .Property(x => x.Email)
            .HasMaxLength(70)
            .IsRequired();
    }
}