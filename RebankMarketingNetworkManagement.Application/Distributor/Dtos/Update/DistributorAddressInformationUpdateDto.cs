using RebankMarketingNetworkManagement.Domain.Enums;

namespace RebankMarketingNetworkManagement.Application.Distributor.Dtos.Update;

public class DistributorAddressInformationUpdateDto
{
    public AddressType? AddressType { get; set; }
    public string? Address { get; set; }
}
