namespace RebankMarketingNetworkManagement.Application.DistributorSale.Dtos;

public class DistributorSaleDto
{
    public Guid DistributorID { get; set; }
    public DateTime SaleDate { get; set; }
    public Guid ProductID { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal SumSalePrice { get; set; }
}
