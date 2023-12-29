using Microsoft.AspNetCore.Mvc;
using RebankMarketingNetworkManagement.Application.Distributor.Commands.AddDistributorCommand;
using RebankMarketingNetworkManagement.Application.Distributor.Commands.DeleteDistributorByIdCommand;
using RebankMarketingNetworkManagement.Application.Distributor.Commands.UpdateDistributorByIdCommand;
using RebankMarketingNetworkManagement.Application.Distributor.Queries.GetAllDistributorsQuery;
using RebankMarketingNetworkManagement.Application.User.Commands.DeleteUserByIdCommand;

namespace RebankMarketingNetworkManagement.Api.Controllers;

[Route("api/distributors")]
public class DistributorController : ApiControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetDistributors([FromQuery] GetDistributorsQuery query, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddDistributor([FromBody] AddDistributorCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{distributorId:guid}")]
    public async Task<IResult> DeleteDistributor(DeleteDistributorByIdCommand command, CancellationToken cancellationToken)
    {
        await Mediator.Send(command, cancellationToken);
        return Results.Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateDistributor([FromBody] UpdateDistributorByIdCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return Ok(result);
    }
}
