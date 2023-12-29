using MediatR;
using RebankMarketingNetworkManagement.Application.Common.Exceptions;
using RebankMarketingNetworkManagement.Application.Common.Interfaces.Repositories.Distributor;
using RebankMarketingNetworkManagement.Application.Distributor.Dtos;
using RebankMarketingNetworkManagement.Application.Distributor.Mapping.Commands;

namespace RebankMarketingNetworkManagement.Application.Distributor.Queries.GetAllDistributorsQuery;

public class GetDistributorsQueryHandler : IRequestHandler<GetDistributorsQuery, List<DistributorDto>>
{
    private readonly IDistributorRepository _distributorRepository;

    public GetDistributorsQueryHandler(IDistributorRepository distributorRepository)
    {
        _distributorRepository = distributorRepository ?? throw new ArgumentNullException(nameof(distributorRepository));
    }

    public async Task<List<DistributorDto>> Handle(GetDistributorsQuery request, CancellationToken cancellationToken)
    {
        var response = await _distributorRepository.GetDistributorsWithAllRelatedEntitiesAsync(cancellationToken);

        if (response is null)
        {
            return new List<DistributorDto>();
        }

        return response
                .Select(p => p.DistributorEntityToDto())
                .ToList();
    }
}