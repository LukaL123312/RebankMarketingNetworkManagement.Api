using RebankMarketingNetworkManagement.Application.Common.Interfaces.Repositories.Distributor;
using RebankMarketingNetworkManagement.Infrastructure.Persistance.Repositories.Base;

namespace RebankMarketingNetworkManagement.Infrastructure.Persistance.Repositories.Distributor;

public class DistributorContactInformationRepository
    : Repository<Domain.DistributorContactInformation>, IDistributorContactInformationRepository
{
    public DistributorContactInformationRepository(
        AppDbContext context)
        : base(context)
    {
    }
}