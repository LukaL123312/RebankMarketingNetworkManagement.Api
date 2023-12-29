using FluentValidation;
using RebankMarketingNetworkManagement.Application.Distributor.Commands.Validators.Common;

namespace RebankMarketingNetworkManagement.Application.DistributorSale.Validators;

public class AddDistributorSaleCommandValidator : AbstractValidator<Commands.AddDistributorSaleCommand.AddDistributorSaleCommand>
{
    public AddDistributorSaleCommandValidator()
    {
        RuleFor(u => u.DistributorID)
            .NotEmpty();

        RuleFor(u => u.SaleDate)
            .NotEmpty()
            .Must(saleDate => saleDate <= DateTime.UtcNow)
            .WithMessage("sale Date cannot be in the future.");

        RuleFor(u => u.ProductCode)
            .NotEmpty()
            .MaximumLength(250);

        RuleFor(u => u.ProductName)
            .NotEmpty()
            .MaximumLength(250);

        RuleFor(u => u.UnitPrice)
            .NotEmpty()
            .GreaterThanOrEqualTo(0)
            .WithMessage("Product can't have negative price");

        RuleFor(u => u.Quantity)
            .NotEmpty()
            .GreaterThanOrEqualTo(0)
            .WithMessage("Distributor couldn't have sold this product less than 0 times");

        RuleFor(u => u.SumSalePrice)
            .NotEmpty()
            .GreaterThanOrEqualTo(0)
            .WithMessage("SumSalePrice can't be negative");
    }
}