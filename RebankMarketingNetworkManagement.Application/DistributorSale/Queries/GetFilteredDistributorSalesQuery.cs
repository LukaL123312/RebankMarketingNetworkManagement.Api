using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RebankMarketingNetworkManagement.Application.DistributorSale.Dtos;

namespace RebankMarketingNetworkManagement.Application.DistributorSale.Queries;

public class GetFilteredDistributorSalesQuery : IRequest<List<DistributorSaleDto>>
{
    [FromQuery(Name = "distributorId")]
    public Guid? DistributorID { get; set; }

    [FromQuery(Name = "startDate")]
    public DateTime? StartDate { get; set; }

    [FromQuery(Name = "endDate")]
    public DateTime? EndDate { get; set; }

    [FromQuery(Name = "productId")]
    public Guid? ProductID { get; set; }
}