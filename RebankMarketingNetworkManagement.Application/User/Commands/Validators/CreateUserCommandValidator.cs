using FluentValidation;

namespace RebankMarketingNetworkManagement.Application.User.Commands.Validators;

public class CreateUserCommandValidator : AbstractValidator<User.Commands.CreateUserCommand.CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(u => u.UserName)
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(20);

        RuleFor(u => u.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(u => u.Password)
            .NotEmpty()
            .NotEqual(p => p.Email)
            .NotEqual(p => p.UserName)
            .Matches("^(?=.*[A-Za-z])(?=.*[^A-Za-z0-9])(?=.*[a-z])(?=.*[A-Z]).{8,}$");
    }
}