using RebankMarketingNetworkManagement.Application.Distributor.Commands.AddDistributorCommand;
using RebankMarketingNetworkManagement.Application.Product.Commands.AddProductCommand;
using RebankMarketingNetworkManagement.Domain;

namespace RebankMarketingNetworkManagement.Application.Product.Mapping;

public static class ProductMappingHelper
{
    public static Domain.Product CommandToEntityMapper(this AddProductCommand addProductCommand)
    {
        return new Domain.Product()
        {
            ProductCode = addProductCommand.ProductCode,
            ProductName = addProductCommand.ProductName,
            UnitPrice = addProductCommand.UnitPrice,
        };
    }
}
