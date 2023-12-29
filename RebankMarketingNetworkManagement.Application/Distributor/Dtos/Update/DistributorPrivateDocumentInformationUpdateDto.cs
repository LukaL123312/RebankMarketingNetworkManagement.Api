using RebankMarketingNetworkManagement.Domain.Enums;

namespace RebankMarketingNetworkManagement.Application.Distributor.Dtos.Update;

public class DistributorPrivateDocumentInformationUpdateDto
{
    public DocumentType? DocumentType { get; set; }
    public string? SerialNumber { get; set; }
    public string? Number { get; set; }
    public DateTime? IssuingDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public string? PrivateNumber { get; set; }
    public string? IssuerOrganization { get; set; }
}