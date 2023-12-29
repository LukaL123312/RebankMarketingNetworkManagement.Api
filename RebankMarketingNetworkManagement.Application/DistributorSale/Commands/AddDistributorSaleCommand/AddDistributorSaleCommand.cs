using MediatR;
using RebankMarketingNetworkManagement.Application.Distributor.Dtos;
using RebankMarketingNetworkManagement.Domain.Enums;

namespace RebankMarketingNetworkManagement.Application.DistributorSale.Commands.AddDistributorSaleCommand;

public class AddDistributorSaleCommand : IRequest
{
    public Guid DistributorID { get; set; }
    public DateTime SaleDate { get; set; }
    public string ProductCode { get; set; }
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal SumSalePrice { get; set; }
}
