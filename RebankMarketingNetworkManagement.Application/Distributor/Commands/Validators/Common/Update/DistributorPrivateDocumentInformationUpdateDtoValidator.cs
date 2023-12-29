using FluentValidation;
using RebankMarketingNetworkManagement.Application.Distributor.Dtos;
using RebankMarketingNetworkManagement.Application.Distributor.Dtos.Update;

namespace RebankMarketingNetworkManagement.Application.Distributor.Commands.Validators.Common.Update;

public class DistributorPrivateDocumentInformationUpdateDtoValidator : AbstractValidator<DistributorPrivateDocumentInformationUpdateDto>
{
    public DistributorPrivateDocumentInformationUpdateDtoValidator()
    {
        RuleFor(dto => dto.DocumentType)
            .IsInEnum()
            .When(dto => dto.DocumentType != null);

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
            .When(dto => dto.IssuingDate != null)
            .WithMessage("Issuing date cannot be in the future.");

        RuleFor(dto => dto.ExpiryDate)
            .NotEmpty()
            .Must(expiryDate => expiryDate >= DateTime.UtcNow)
            .When(dto => dto.ExpiryDate != null)
            .WithMessage("Expiry date must be in the future.");

        RuleFor(dto => dto.PrivateNumber)
            .NotEmpty()
            .MaximumLength(50)
            .When(dto => !string.IsNullOrEmpty(dto.PrivateNumber));

        RuleFor(dto => dto.IssuerOrganization)
            .MaximumLength(100)
            .When(dto => dto.IssuerOrganization != null)
            .WithMessage("Issuer should be less than or equal to 100 characters when not null.");
    }
}