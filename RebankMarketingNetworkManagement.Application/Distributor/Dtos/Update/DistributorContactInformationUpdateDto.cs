using RebankMarketingNetworkManagement.Domain.Enums;

namespace RebankMarketingNetworkManagement.Application.Distributor.Dtos.Update;

public class DistributorContactInformationUpdateDto
{
    public ContactType? ContactType { get; set; }
    public string? ContactInformation { get; set; }
}