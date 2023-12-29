using RebankMarketingNetworkManagement.Domain.Common;

namespace RebankMarketingNetworkManagement.Domain;

public class Product : AuditableEntity
{
    public Guid ProductID { get; set; }
    public string ProductCode { get; set; }
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }

    public ICollection<DistributorSale>? DistributorSales { get; set; }
}
