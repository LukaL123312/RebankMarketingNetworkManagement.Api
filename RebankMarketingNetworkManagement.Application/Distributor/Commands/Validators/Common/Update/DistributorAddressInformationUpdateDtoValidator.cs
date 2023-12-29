using FluentValidation;
using RebankMarketingNetworkManagement.Application.Distributor.Dtos;
using RebankMarketingNetworkManagement.Application.Distributor.Dtos.Update;

namespace RebankMarketingNetworkManagement.Application.Distributor.Commands.Validators.Common.Update;

public class DistributorAddressInformationUpdateDtoValidator : AbstractValidator<DistributorAddressInformationUpdateDto>
{
    public DistributorAddressInformationUpdateDtoValidator()
    {
        RuleFor(dto => dto.AddressType)
            .IsInEnum()
            .When(u => u.AddressType != null);

        RuleFor(dto => dto.Address)
            .NotEmpty()
            .MaximumLength(100)
            .When(u => !string.IsNullOrEmpty(u.Address));
    }
}
