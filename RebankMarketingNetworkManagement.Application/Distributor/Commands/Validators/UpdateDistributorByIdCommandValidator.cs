using FluentValidation;
using RebankMarketingNetworkManagement.Application.Distributor.Commands.Validators.Common;
using RebankMarketingNetworkManagement.Application.Distributor.Commands.Validators.Common.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RebankMarketingNetworkManagement.Application.Distributor.Commands.Validators;

public class UpdateDistributorByIdCommandValidator : AbstractValidator<Commands.UpdateDistributorByIdCommand.UpdateDistributorByIdCommand>
{
    public UpdateDistributorByIdCommandValidator()
    {
        RuleFor(u => u.DistributorID)
            .NotEmpty();

        RuleFor(u => u.Name)
            .NotEmpty()
            .MaximumLength(50)
            .When(u => !string.IsNullOrEmpty(u.Name));

        RuleFor(u => u.Surname)
            .NotEmpty()
            .MaximumLength(50)
            .When(u => !string.IsNullOrEmpty(u.Surname));

        RuleFor(u => u.BirthDate)
            .NotEmpty()
            .Must(birthDate => birthDate <= DateTime.UtcNow)
            .When(u => u.BirthDate != null)
            .WithMessage("Birth date cannot be in the future.");

        RuleFor(u => u.Gender)
            .IsInEnum()
            .When(u => u.Gender != null);

        RuleFor(u => u.PrivateDocumentInformation)
            .SetValidator(new DistributorPrivateDocumentInformationUpdateDtoValidator())
            .When(u => u.PrivateDocumentInformation != null);

        RuleFor(u => u.ContactInformation)
            .SetValidator(new DistributorContactInformationUpdateDtoValidator())
            .When(u => u.ContactInformation != null);


        RuleFor(u => u.AddressInformation)
            .SetValidator(new DistributorAddressInformationUpdateDtoValidator())
            .When(u => u.AddressInformation != null);
    }
}