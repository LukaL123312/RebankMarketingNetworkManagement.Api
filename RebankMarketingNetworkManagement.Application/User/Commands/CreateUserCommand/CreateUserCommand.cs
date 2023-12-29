using MediatR;

namespace RebankMarketingNetworkManagement.Application.User.Commands.CreateUserCommand;
public class CreateUserCommand : IRequest<string>
{
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}