using FluentValidation;
using RebankMarketingNetworkManagement.Application.Distributor.Dtos;

namespace RebankMarketingNetworkManagement.Application.Distributor.Commands.Validators.Common;

public class DistributorAddressInformationDtoValidator : AbstractValidator<DistributorAddressInformationDto>
{
    public DistributorAddressInformationDtoValidator()
    {
        RuleFor(dto => dto.AddressType)
            .IsInEnum();

        RuleFor(dto => dto.Address)
            .NotEmpty()
            .MaximumLength(100);
    }
}
