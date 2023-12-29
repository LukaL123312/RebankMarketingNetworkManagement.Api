using FluentValidation;
using RebankMarketingNetworkManagement.Application.Distributor.Dtos;
using RebankMarketingNetworkManagement.Application.Distributor.Dtos.Update;

namespace RebankMarketingNetworkManagement.Application.Distributor.Commands.Validators.Common.Update;


public class DistributorContactInformationUpdateDtoValidator : AbstractValidator<DistributorContactInformationUpdateDto>
{
    public DistributorContactInformationUpdateDtoValidator()
    {
        RuleFor(dto => dto.ContactType)
            .IsInEnum()
            .When(dto => dto.ContactType != null);

        RuleFor(dto => dto.ContactInformation)
            .NotEmpty()
            .MaximumLength(100)
            .When(dto => !string.IsNullOrEmpty(dto.ContactInformation));
    }
}