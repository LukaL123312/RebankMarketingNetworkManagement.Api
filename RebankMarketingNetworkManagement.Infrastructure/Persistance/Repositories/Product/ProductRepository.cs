using Microsoft.EntityFrameworkCore;
using RebankMarketingNetworkManagement.Application.Common.Interfaces.Repositories.Distributor;
using RebankMarketingNetworkManagement.Application.Common.Interfaces.Repositories.Product;
using RebankMarketingNetworkManagement.Infrastructure.Persistance.Repositories.Base;

namespace RebankMarketingNetworkManagement.Infrastructure.Persistance.Repositories.Product;

public class ProductRepository
    : Repository<Domain.Product>, IProductRepository
{
    public ProductRepository(
        AppDbContext context)
        : base(context)
    {
    }

    public Task<bool> DoesProductExist(string productCode, string productName)
    {
        return DbSet.AnyAsync(u => u.ProductCode.ToLower().Equals(productCode.ToLower()) &&
            u.ProductName.ToLower().Equals(productName.ToLower()));
    }
}