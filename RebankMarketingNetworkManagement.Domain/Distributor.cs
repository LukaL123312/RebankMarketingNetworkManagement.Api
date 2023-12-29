using RebankMarketingNetworkManagement.Domain.Common;
using RebankMarketingNetworkManagement.Domain.Enums;

namespace RebankMarketingNetworkManagement.Domain;

public class Distributor : AuditableEntity
{
    public Guid DistributorID { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public string? Photo { get; set; } // store base64 version of byte array

    public DistributorPrivateDocumentInformation PrivateDocumentInformation { get; set; }
    public DistributorContactInformation ContactInformation { get; set; }
    public DistributorAddressInformation AddressInformation { get; set; }


    // Navigation property for self-referencing relationship (recommendation tree)
    public Guid? RecommenderID { get; set; }
    public virtual Distributor? Recommender { get; set; }
    public virtual ICollection<Distributor>? RecommendedDistributors { get; set; }

    public virtual ICollection<DistributorSale>? DistributorSales { get; set; }
    public virtual ICollection<DistributorBonus>? DistributorBonus { get; set; }

    public int? RecommendedCount { get; set; } = 0;
}
