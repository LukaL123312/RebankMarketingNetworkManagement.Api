using MediatR;
using Microsoft.EntityFrameworkCore;
using RebankMarketingNetworkManagement.Application.Common.Exceptions;
using RebankMarketingNetworkManagement.Application.Common.Interfaces.Repositories.Distributor;
using RebankMarketingNetworkManagement.Application.Distributor.Exceptions;
using RebankMarketingNetworkManagement.Infrastructure.Persistance.Repositories.Base;
using System.Threading;

namespace RebankMarketingNetworkManagement.Infrastructure.Persistance.Repositories.Distributor;

public class DistributorRepository
    : Repository<Domain.Distributor>, IDistributorRepository
{
    public DistributorRepository(
        AppDbContext context)
        : base(context)
    {
    }

    public Task<bool> DoesDistributorExistByPrivateNumberAsync(string privateNumber)
    {
        return DbSet.AnyAsync(u => u.PrivateDocumentInformation.PrivateNumber.Equals(privateNumber));
    }

    public Task<bool> DoesDistributorExistByDistributorIdAsync(Guid distributorID)
    {
        return DbSet.AnyAsync(u => u.DistributorID.Equals(distributorID));
    }

    public async Task<int> CalculateTreeDepthAsync(Domain.Distributor distributor, CancellationToken cancellationToken)
    {
        if (distributor.RecommenderID is null)
        {
            return 1;
        }

        var recommenderDistributor = await DbSet
            .Where(x => x.DistributorID == distributor.RecommenderID)
            .SingleOrDefaultAsync(cancellationToken);

        if (recommenderDistributor != null)
        {
            int childDepth = await CalculateTreeDepthAsync(recommenderDistributor, cancellationToken);
            return 1 + childDepth;
        }

        return 0;
    }

    public async Task<Domain.Distributor?> GetRecommendationTreeRootAsync(Domain.Distributor distributor, CancellationToken cancellationToken)
    {
        if (distributor is null)
        {
            return null;
        }

        await DbSet
            .Where(d => d.DistributorID == distributor.DistributorID)
            .Include(d => d.RecommendedDistributors)
            .LoadAsync(cancellationToken);

        // Traverse upwards to find the root
        Domain.Distributor currentDistributor = distributor;

        while (currentDistributor.RecommenderID != null)
        {
            currentDistributor = await DbSet
                .Where(d => d.DistributorID == currentDistributor.RecommenderID)
                .SingleAsync(cancellationToken);

            await DbSet
                .Where(d => d.DistributorID == currentDistributor.DistributorID)
                .Include(d => d.RecommendedDistributors)
                .LoadAsync(cancellationToken);
        }

        return currentDistributor; // Return reference to the root distributor
    }

    public async Task<int> CalculateTotalTreeNodesAmountAsync(Domain.Distributor? rootDistributor, CancellationToken cancellationToken)
    {
        if (rootDistributor == null)
        {
            return 0;
        }

        int count = 1; // Count the root

        rootDistributor.RecommendedDistributors = await DbSet.Where(x => x.RecommenderID == rootDistributor.DistributorID).ToListAsync();

        if (!rootDistributor.RecommendedDistributors.Any() && rootDistributor.RecommenderID == null)
        {
            return 1; // Base Case: When there is only the root node and the new node is getting added to it
        }

        foreach (var recommendedDistributor in rootDistributor.RecommendedDistributors)
        {
            count += await CalculateTotalTreeNodesAmountAsync(recommendedDistributor, cancellationToken);
        }

        return count;
    }

    public async Task<bool> IsDistributorEligibileAsync(Domain.Distributor recommender, CancellationToken cancellationToken)
    {
        if (recommender is null)
        {
            throw new NotFoundException($"Recommender Distributor wasn't found");
        }

        if (recommender.RecommendedCount == 3)
        {
            throw new RecommendationLimitExceededException("Recommender has already recommended the maximum number of (3) distributors");
        }

        var distributorRecommendationTreeDepth = await CalculateTreeDepthAsync(recommender, cancellationToken);

        if (distributorRecommendationTreeDepth >= 5)
        {
            throw new RecommendationDepthExceededException("Recommendation Depth for a Distributor can not be more than 5");
        }

        var recommendationTreeRoot = await GetRecommendationTreeRootAsync(recommender, cancellationToken);

        var totalDistributorsInRecommendationTree = await CalculateTotalTreeNodesAmountAsync(recommendationTreeRoot, cancellationToken);

        if (totalDistributorsInRecommendationTree + 1 > 121) // if total number of nodes + the node we are inserting is over 121, then it is a violation of a business rule
        {
            throw new RecommendationTreeNodesAmountExceededException("Recommendation Tree Nodes Amount can not be more than 121");
        }

        return true;
    }

    public async Task<Domain.Distributor> GetDistributorWithRelatedInformationEntitiesByDistributorIdAsync(Guid distributorID, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Where(d => d.DistributorID == distributorID)
            .Include(d => d.AddressInformation)  // Include AddressInformation
            .Include(d => d.PrivateDocumentInformation)  // Include PrivateDocumentInformation
            .Include(d => d.ContactInformation)  // Include ContactInformation
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<Domain.Distributor>> GetDistributorsWithAllRelatedEntitiesAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(d => d.AddressInformation)  // Include AddressInformation
            .Include(d => d.PrivateDocumentInformation)  // Include PrivateDocumentInformation
            .Include(d => d.ContactInformation)  // Include ContactInformation
            .Include(d => d.RecommendedDistributors) // Include RecommendedDistributors
            .ToListAsync(cancellationToken);
    }

    public async Task<Domain.Distributor> GetDistributorWithAllRelatedEntitiesByDistributorIdAsync(Guid distributorId,
        CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Where(x => x.DistributorID == distributorId)
            .Include(d => d.AddressInformation)  // Include AddressInformation
            .Include(d => d.PrivateDocumentInformation)  // Include PrivateDocumentInformation
            .Include(d => d.ContactInformation)  // Include ContactInformation
            .Include(d => d.RecommendedDistributors) // Include RecommendedDistributors
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<Domain.Distributor>> GetDistributorsWithAllRelatedEntitiesByDistributorIdsAsync(List<Guid> distributorIds,
    CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Where(d => distributorIds.Contains(d.DistributorID))
            .Include(d => d.AddressInformation)  // Include AddressInformation
            .Include(d => d.PrivateDocumentInformation)  // Include PrivateDocumentInformation
            .Include(d => d.ContactInformation)  // Include ContactInformation
            .Include(d => d.RecommendedDistributors) // Include RecommendedDistributors
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Domain.Distributor>> GetFirstGenerationRecommendedDistributorsByDistributorIdAsync(Guid distributorId,
        CancellationToken cancellationToken = default)
    {
        var distributor = await GetDistributorWithAllRelatedEntitiesByDistributorIdAsync(distributorId, cancellationToken);

        if(distributor is null)
        {
            return new List<Domain.Distributor>();
        }

        if(distributor.RecommendedDistributors is null)
        {
            return new List<Domain.Distributor>();
        }

        return distributor.RecommendedDistributors.ToList();
    }

    public async Task<List<Domain.Distributor>> GetSecondGenerationRecommendedDistributorsByDistributorIdAsync(Guid distributorId,
    CancellationToken cancellationToken = default)
    {
        List<Domain.Distributor>? secondGenRecommendedDistributors = new List<Domain.Distributor>();

        var firstGenRecommendedDistributors = await GetFirstGenerationRecommendedDistributorsByDistributorIdAsync(distributorId, cancellationToken);

        if (firstGenRecommendedDistributors.Any())
        {
            foreach(var firstGenRecommendedDistributor in firstGenRecommendedDistributors)
            {
                var firstGenRecommendedDistributorRecommendations =
                    await GetFirstGenerationRecommendedDistributorsByDistributorIdAsync(firstGenRecommendedDistributor.DistributorID);

                secondGenRecommendedDistributors.AddRange(firstGenRecommendedDistributorRecommendations);
            }
        }

        return secondGenRecommendedDistributors;
    }

    public async Task<List<Domain.Distributor>> GetDistributorsByIdsAsync(List<Guid> distributorIds)
    {
        return await DbSet
            .Where(d => distributorIds.Contains(d.DistributorID))
            .ToListAsync();
    }
}