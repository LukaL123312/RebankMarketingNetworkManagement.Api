using RebankMarketingNetworkManagement.Domain.Enums;

namespace RebankMarketingNetworkManagement.Application.Distributor.Dtos;

public class DistributorAddressInformationDto
{
    public AddressType AddressType { get; set; }
    public string Address { get; set; }
}
