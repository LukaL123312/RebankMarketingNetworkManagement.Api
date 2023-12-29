using FluentValidation;

namespace RebankMarketingNetworkManagement.Application.Product.Commands.Validators;

public class AddProductCommandValidator : AbstractValidator<AddProductCommand.AddProductCommand>
{
    public AddProductCommandValidator()
    {
        RuleFor(x => x.ProductCode)
            .NotEmpty()
            .NotNull()
            .Must(x => x.Length <= 250);

        RuleFor(x => x.ProductName)
            .NotEmpty()
            .NotNull()
            .Must(x => x.Length <= 250);

        RuleFor(x => x.UnitPrice)
            .NotEmpty();
    }
}