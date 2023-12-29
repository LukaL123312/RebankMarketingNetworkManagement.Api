using Microsoft.AspNetCore.Mvc;
using RebankMarketingNetworkManagement.Application.DistributorSale.Commands.AddDistributorSaleCommand;
using RebankMarketingNetworkManagement.Application.DistributorSale.Queries;

namespace RebankMarketingNetworkManagement.Api.Controllers;

[Route("api/distributorSales")]
public class DistributorSaleController : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddDistributorSale([FromBody] AddDistributorSaleCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetFilteredDistributorSales([FromQuery] GetFilteredDistributorSalesQuery query, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(query, cancellationToken);
        return Ok(result);
    }
}
