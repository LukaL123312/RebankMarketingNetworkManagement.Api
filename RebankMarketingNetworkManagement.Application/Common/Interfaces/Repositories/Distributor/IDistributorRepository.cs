using RebankMarketingNetworkManagement.Application.Common.Interfaces.Repositories.Base;

namespace RebankMarketingNetworkManagement.Application.Common.Interfaces.Repositories.Distributor;

public interface IDistributorRepository : IRepository<Domain.Distributor>
{
    Task<Domain.Distributor> GetDistributorWithRelatedInformationEntitiesByDistributorIdAsync(Guid distributorID, 
        CancellationToken cancellationToken = default);
    Task<bool> DoesDistributorExistByPrivateNumberAsync(string privateNumber);
    Task<bool> DoesDistributorExistByDistributorIdAsync(Guid distributorID);
    public Task<int> CalculateTreeDepthAsync(Domain.Distributor distributor, CancellationToken cancellationToken);
    public Task<Domain.Distributor?> GetRecommendationTreeRootAsync(Domain.Distributor distributor, CancellationToken cancellationToken);
    public Task<int> CalculateTotalTreeNodesAmountAsync(Domain.Distributor? rootDistributor, CancellationToken cancellationToken);
    Task<bool> IsDistributorEligibileAsync(Domain.Distributor recommender, CancellationToken cancellationToken);
    Task<List<Domain.Distributor>> GetDistributorsWithAllRelatedEntitiesAsync(CancellationToken cancellationToken = default);
    Task<Domain.Distributor> GetDistributorWithAllRelatedEntitiesByDistributorIdAsync(Guid distributorId,
        CancellationToken cancellationToken = default);
    Task<List<Domain.Distributor>> GetDistributorsWithAllRelatedEntitiesByDistributorIdsAsync(List<Guid> distributorIds,
        CancellationToken cancellationToken = default);
    Task<List<Domain.Distributor>> GetFirstGenerationRecommendedDistributorsByDistributorIdAsync(Guid distributorId,
        CancellationToken cancellationToken = default);
    Task<List<Domain.Distributor>> GetSecondGenerationRecommendedDistributorsByDistributorIdAsync(Guid distributorId,
        CancellationToken cancellationToken = default);

    Task<List<Domain.Distributor>> GetDistributorsByIdsAsync(List<Guid> distributorIds);
}