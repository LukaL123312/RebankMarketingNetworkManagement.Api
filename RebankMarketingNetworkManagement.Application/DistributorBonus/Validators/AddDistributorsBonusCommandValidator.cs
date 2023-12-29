using FluentValidation;

namespace RebankMarketingNetworkManagement.Application.DistributorBonus.Validators;

public class AddDistributorsBonusCommandValidator : AbstractValidator<Commands.AddDistributorsBonusCommand>
{
    public AddDistributorsBonusCommandValidator()
    {
        RuleFor(u => u.StartDate)
            .NotEmpty()
            .Must(startDate => startDate <= DateTime.UtcNow)
            .WithMessage("Start Date cannot be in the future.");

        RuleFor(u => u.EndDate)
            .NotEmpty();
    }
}