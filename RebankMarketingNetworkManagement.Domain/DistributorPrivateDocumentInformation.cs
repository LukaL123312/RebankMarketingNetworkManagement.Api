using RebankMarketingNetworkManagement.Domain.Common;
using RebankMarketingNetworkManagement.Domain.Enums;

namespace RebankMarketingNetworkManagement.Domain;

public class DistributorPrivateDocumentInformation : EntityBase
{
    public Guid DistributorPrivateDocumentInformationID { get; set; }
    public Guid DistributorID { get; set; }
    public DocumentType DocumentType { get; set; }
    public string? SerialNumber { get; set; }
    public string? Number { get; set; }
    public DateTime IssuingDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public string PrivateNumber { get; set; }
    public string? IssuerOrganization { get; set; }

    public virtual Distributor Distributor { get; set; }
}
