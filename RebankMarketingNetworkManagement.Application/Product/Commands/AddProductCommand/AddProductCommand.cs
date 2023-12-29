using MediatR;

namespace RebankMarketingNetworkManagement.Application.Product.Commands.AddProductCommand;

public class AddProductCommand : IRequest<Unit>
{
    public string ProductCode { get; set; }
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
}
