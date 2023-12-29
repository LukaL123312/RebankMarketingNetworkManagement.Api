using FluentValidation;
using RebankMarketingNetworkManagement.Application.Distributor.Commands.Validators.Common;

namespace RebankMarketingNetworkManagement.Application.Distributor.Commands.Validators;

public class AddDistributorCommandValidator : AbstractValidator<Commands.AddDistributorCommand.AddDistributorCommand>
{
    public AddDistributorCommandValidator()
    {
        RuleFor(u => u.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(u => u.Surname)
            .NotEmpty()
             .MaximumLength(50);

        RuleFor(u => u.BirthDate)
            .NotEmpty()
            .Must(birthDate => birthDate <= DateTime.UtcNow)
            .WithMessage("Birth date cannot be in the future.");

        RuleFor(u => u.Gender)
            .IsInEnum();

        RuleFor(u => u.PrivateDocumentInformation)
            .SetValidator(new DistributorPrivateDocumentInformationDtoValidator());

        RuleFor(u => u.ContactInformation)
            .SetValidator(new DistributorContactInformationDtoValidator());

        RuleFor(u => u.AddressInformation)
            .SetValidator(new DistributorAddressInformationDtoValidator());
    }
}