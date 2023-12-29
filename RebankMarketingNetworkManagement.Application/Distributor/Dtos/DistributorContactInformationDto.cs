using RebankMarketingNetworkManagement.Domain.Enums;

namespace RebankMarketingNetworkManagement.Application.Distributor.Dtos;

public class DistributorContactInformationDto
{
    public ContactType ContactType { get; set; }
    public string ContactInformation { get; set; }
}
