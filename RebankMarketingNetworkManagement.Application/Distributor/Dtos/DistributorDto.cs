using RebankMarketingNetworkManagement.Domain.Enums;

namespace RebankMarketingNetworkManagement.Application.Distributor.Dtos;

public class DistributorDto
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public string? Photo { get; set; } // store base64 string version of the image
    public DistributorPrivateDocumentInformationDto PrivateDocumentInformation { get; set; }
    public DistributorContactInformationDto ContactInformation { get; set; }
    public DistributorAddressInformationDto AddressInformation { get; set; }
    public Guid? RecommenderID { get; set; }
    public List<Guid> RecommendedDistributorIDs { get; set; }
}
