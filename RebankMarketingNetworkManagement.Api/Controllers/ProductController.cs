using Microsoft.AspNetCore.Mvc;
using RebankMarketingNetworkManagement.Application.Product.Commands.AddProductCommand;

namespace RebankMarketingNetworkManagement.Api.Controllers;

[Route("api/products")]
public class ProductController : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] AddProductCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return Ok(result);
    }
}
