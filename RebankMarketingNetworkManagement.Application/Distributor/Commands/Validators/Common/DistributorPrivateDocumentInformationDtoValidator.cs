using FluentValidation;
using RebankMarketingNetworkManagement.Application.Distributor.Dtos;

namespace RebankMarketingNetworkManagement.Application.Distributor.Commands.Validators.Common;

public class DistributorPrivateDocumentInformationDtoValidator : AbstractValidator<DistributorPrivateDocumentInformationDto>
{
    public DistributorPrivateDocumentInformationDtoValidator()
    {
        RuleFor(dto => dto.DocumentType)
            .IsInEnum();

        RuleFor(dto => dto.SerialNumber)
            .MaximumLength(10)
            .When(dto => dto.SerialNumber != null)
            .WithMessage("Document Serial number should be less than or equal to 10 characters when not null.");

        RuleFor(dto => dto.Number)
            .MaximumLength(10)
            .When(dto => dto.Number != null)
            .WithMessage("Document Number should be less than or equal to 10 characters when not null.");

        RuleFor(dto => dto.IssuingDate)
            .NotEmpty()
            .Must(issuingDate => issuingDate <= DateTime.UtcNow)
            .WithMessage("Issuing date cannot be in the future.");

        RuleFor(dto => dto.ExpiryDate)
            .NotEmpty()
            .Must(expiryDate => expiryDate >= DateTime.UtcNow)
            .WithMessage("Expiry date must be in the future.");

        RuleFor(dto => dto.PrivateNumber)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(dto => dto.IssuerOrganization)
            .MaximumLength(100)
            .When(dto => dto.IssuerOrganization != null)
            .WithMessage("Issuer should be less than or equal to 100 characters when not null.");
    }
}
