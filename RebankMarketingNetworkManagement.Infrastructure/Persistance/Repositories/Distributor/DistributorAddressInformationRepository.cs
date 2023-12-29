using Microsoft.EntityFrameworkCore;
using RebankMarketingNetworkManagement.Application.Common.Interfaces.Repositories.Distributor;
using RebankMarketingNetworkManagement.Application.Common.Interfaces.Repositories.User;
using RebankMarketingNetworkManagement.Infrastructure.Persistance.Repositories.Base;

namespace RebankMarketingNetworkManagement.Infrastructure.Persistance.Repositories.Distributor;

public class DistributorAddressInformationRepository
    : Repository<Domain.DistributorAddressInformation>, IDistributorAddressInformationRepository
{
    public DistributorAddressInformationRepository(
        AppDbContext context)
        : base(context)
    {
    }
}