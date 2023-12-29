using FluentValidation;
using RebankMarketingNetworkManagement.Application.Distributor.Dtos;

namespace RebankMarketingNetworkManagement.Application.Distributor.Commands.Validators.Common;

public class DistributorContactInformationDtoValidator : AbstractValidator<DistributorContactInformationDto>
{
    public DistributorContactInformationDtoValidator()
    {
        RuleFor(dto => dto.ContactType)
            .IsInEnum();

        RuleFor(dto => dto.ContactInformation)
            .NotEmpty()
            .MaximumLength(100);
    }
}
