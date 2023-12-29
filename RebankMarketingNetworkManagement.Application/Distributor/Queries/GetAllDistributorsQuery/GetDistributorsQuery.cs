using MediatR;
using RebankMarketingNetworkManagement.Application.Distributor.Dtos;

namespace RebankMarketingNetworkManagement.Application.Distributor.Queries.GetAllDistributorsQuery;

public class GetDistributorsQuery : IRequest<List<DistributorDto>>
{
}