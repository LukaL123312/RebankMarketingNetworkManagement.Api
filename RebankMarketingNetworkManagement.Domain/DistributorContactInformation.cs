using RebankMarketingNetworkManagement.Domain.Common;
using RebankMarketingNetworkManagement.Domain.Enums;

namespace RebankMarketingNetworkManagement.Domain;

public class DistributorContactInformation : EntityBase
{
    public Guid DistributorContactInformationID { get; set; }
    public Guid DistributorID { get; set; }
    public ContactType ContactType { get; set; }
    public string ContactInformation { get; set; }

    public virtual Distributor Distributor { get; set; }
}
