using MediatR;
using RebankMarketingNetworkManagement.Application.Common.Interfaces.Repositories.Distributor;
using RebankMarketingNetworkManagement.Application.Distributor.Dtos;
using RebankMarketingNetworkManagement.Application.Distributor.Queries.GetAllDistributorsQuery;
using RebankMarketingNetworkManagement.Application.DistributorSale.Dtos;
using RebankMarketingNetworkManagement.Application.DistributorSale.Mapping;

namespace RebankMarketingNetworkManagement.Application.DistributorSale.Queries;

public class GetFilteredDistributorSalesQueryHandler : IRequestHandler<GetFilteredDistributorSalesQuery, List<DistributorSaleDto>>
{
    private readonly IDistributorSaleRepository _distributorSaleRepository;

    public GetFilteredDistributorSalesQueryHandler(IDistributorSaleRepository distributorSaleRepository)
    {
        _distributorSaleRepository = distributorSaleRepository ?? throw new ArgumentNullException(nameof(distributorSaleRepository));
    }

    public async Task<List<DistributorSaleDto>> Handle(GetFilteredDistributorSalesQuery request, CancellationToken cancellationToken)
    {
        var response = await _distributorSaleRepository.GetFilteredDistributorsAsync(request.DistributorID, request.StartDate,
            request.EndDate, request.ProductID);

        if (response is null)
        {
            return new List<DistributorSaleDto>();
        }

        return response
                .Select(p => p.DistributorEntityToDto())
                .ToList();
    }




}