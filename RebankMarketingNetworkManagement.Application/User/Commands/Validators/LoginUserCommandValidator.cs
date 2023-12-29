using FluentValidation;

namespace RebankMarketingNetworkManagement.Application.User.Commands.Validators;

public class LoginUserCommandValidator : AbstractValidator<User.Commands.LoginUserCommand.LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}
