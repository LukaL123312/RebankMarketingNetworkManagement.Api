using RebankMarketingNetworkManagement.Domain.Common;
using RebankMarketingNetworkManagement.Domain.Enums;

namespace RebankMarketingNetworkManagement.Domain;

public class DistributorAddressInformation : EntityBase
{
    public Guid DistributorAddressInformationID { get; set; }
    public Guid DistributorID { get; set; }
    public AddressType AddressType { get; set; }
    public string Address { get; set; }

    public virtual Distributor Distributor { get; set; }
}
