using FluentValidation;
using MediatR;

namespace RebankMarketingNetworkManagement.Application.User.Commands.LoginUserCommand;

public class LoginUserCommand : IRequest<string>
{
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
}