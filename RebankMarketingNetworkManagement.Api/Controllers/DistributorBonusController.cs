using Microsoft.AspNetCore.Mvc;
using RebankMarketingNetworkManagement.Application.Distributor.Commands.AddDistributorCommand;
using RebankMarketingNetworkManagement.Application.DistributorBonus.Commands;
using RebankMarketingNetworkManagement.Application.DistributorBonus.Queries;

namespace RebankMarketingNetworkManagement.Api.Controllers;

[Route("api/distributorBonuses")]
public class DistributorBonusController : ApiControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetFilteredDistributorBonuses([FromQuery] GetFilteredDistributorBonusesQuery query, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(query, cancellationToken);
        return Ok(result);
    }


    [HttpPost]
    public async Task<IActionResult> CalculateDistributorBonuses([FromBody] AddDistributorsBonusCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return Ok(result);
    }
}