using RebankMarketingNetworkManagement.Application.Common.Interfaces.Repositories.Distributor;
using RebankMarketingNetworkManagement.Infrastructure.Persistance.Repositories.Base;

namespace RebankMarketingNetworkManagement.Infrastructure.Persistance.Repositories.Distributor;

public class DistributorPrivateDocumentInformationRepository
    : Repository<Domain.DistributorPrivateDocumentInformation>, IDistributorPrivateDocumentInformationRepository
{
    public DistributorPrivateDocumentInformationRepository(
        AppDbContext context)
        : base(context)
    {
    }
}