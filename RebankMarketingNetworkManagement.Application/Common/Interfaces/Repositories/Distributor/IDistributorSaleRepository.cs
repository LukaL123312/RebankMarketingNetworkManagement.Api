using RebankMarketingNetworkManagement.Application.Common.Interfaces.Repositories.Base;

namespace RebankMarketingNetworkManagement.Application.Common.Interfaces.Repositories.Distributor;

public interface IDistributorSaleRepository : IRepository<Domain.DistributorSale>
{
    Task<IEnumerable<Domain.DistributorSale>> GetFilteredDistributorsAsync(Guid? distributorId, DateTime? startDate, DateTime? endDate, Guid? productId);
}