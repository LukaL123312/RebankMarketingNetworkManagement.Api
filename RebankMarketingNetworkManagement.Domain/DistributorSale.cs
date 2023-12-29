using RebankMarketingNetworkManagement.Domain.Common;

namespace RebankMarketingNetworkManagement.Domain;

public class DistributorSale : AuditableEntity
{
    public Guid DistributorSaleID { get; set; }
    public Guid DistributorID { get; set; }
    public DateTime SaleDate { get; set; }
    public Guid ProductID { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal SumSalePrice { get; set; }
    public bool IsCountedForBonusCalculation { get; set; }
    public virtual Distributor Distributor { get; set; }
    public virtual Product Product { get; set; }
}
