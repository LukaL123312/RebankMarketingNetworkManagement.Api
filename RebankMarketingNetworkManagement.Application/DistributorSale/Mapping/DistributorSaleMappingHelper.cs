using RebankMarketingNetworkManagement.Application.Distributor.Dtos;
using RebankMarketingNetworkManagement.Application.DistributorSale.Commands.AddDistributorSaleCommand;
using RebankMarketingNetworkManagement.Application.DistributorSale.Dtos;

namespace RebankMarketingNetworkManagement.Application.DistributorSale.Mapping;

public static class DistributorSaleMappingHelper
{
    public static Domain.DistributorSale CommandToEntityMapper(this AddDistributorSaleCommand addDistributorSaleCommand, Guid productID)
    {
        return new Domain.DistributorSale()
        {
            DistributorID = addDistributorSaleCommand.DistributorID,
            SaleDate = addDistributorSaleCommand.SaleDate,
            ProductID = productID,
            UnitPrice = addDistributorSaleCommand.UnitPrice,
            Quantity = addDistributorSaleCommand.Quantity,
            SumSalePrice = CheckSumSalePriceValidity(addDistributorSaleCommand.SumSalePrice,
            addDistributorSaleCommand.UnitPrice, addDistributorSaleCommand.Quantity),
        };
    }

    public static decimal CheckSumSalePriceValidity(decimal originalSumSalePrice, decimal unitPrice, int quantity)
    {
        decimal localSumSalePrice = unitPrice * quantity;

        if(localSumSalePrice == originalSumSalePrice)
        {
            return originalSumSalePrice;
        }
        return localSumSalePrice;
    }

    public static DistributorSaleDto DistributorEntityToDto(this Domain.DistributorSale entity)
    {
        return new DistributorSaleDto
        {
            DistributorID = entity.DistributorID,
            UnitPrice = entity.UnitPrice,
            Quantity = entity.Quantity,
            ProductID = entity.ProductID,
            SaleDate = entity.SaleDate,
            SumSalePrice = entity.SumSalePrice  
        };
    }
}
