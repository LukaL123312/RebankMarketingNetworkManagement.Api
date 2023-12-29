using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RebankMarketingNetworkManagement.Application.User.Commands.CreateUserCommand;
using RebankMarketingNetworkManagement.Application.User.Commands.DeleteUserByIdCommand;
using RebankMarketingNetworkManagement.Application.User.Commands.LoginUserCommand;

namespace RebankMarketingNetworkManagement.Api.Controllers;

[Route("api/users")]
public class UserController : ApiControllerBase
{
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IResult> Login(LoginUserCommand command, CancellationToken cancellationToken)
    {
        var accessToken = await Mediator.Send(command, cancellationToken);
        return Results.Ok(accessToken);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IResult> CreateUser(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var accessToken = await Mediator.Send(command, cancellationToken);
        return Results.Ok(accessToken);
    }

    [HttpDelete("{userId:guid}")]
    public async Task<IResult> DeleteUser(DeleteUserByIdCommand command, CancellationToken cancellationToken)
    {
        await Mediator.Send(command, cancellationToken);
        return Results.Ok();
    }
}
