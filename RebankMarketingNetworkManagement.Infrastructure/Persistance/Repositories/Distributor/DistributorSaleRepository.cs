using Microsoft.EntityFrameworkCore;
using RebankMarketingNetworkManagement.Application.Common.Interfaces.Repositories.Distributor;
using RebankMarketingNetworkManagement.Infrastructure.Persistance.Repositories.Base;

namespace RebankMarketingNetworkManagement.Infrastructure.Persistance.Repositories.Distributor;

public class DistributorSaleRepository
    : Repository<Domain.DistributorSale>, IDistributorSaleRepository
{
    public DistributorSaleRepository(
        AppDbContext context)
        : base(context)
    {
    }

    public async Task<IEnumerable<Domain.DistributorSale>> GetFilteredDistributorsAsync(Guid? distributorId, DateTime? startDate, DateTime? endDate, Guid? productId)
    {
        var query = DbSet.AsQueryable();

        if (distributorId != null)
        {
            query = query.Where(d => d.DistributorID == distributorId);
        }

        if (startDate != null)
        {
            query = query.Where(d => d.SaleDate >= startDate);
        }

        if (endDate != null)
        {
            query = query.Where(d => d.SaleDate <= endDate);
        }

        if (productId != null)
        {
            query = query.Where(d => d.ProductID == productId);
        }

        return await query.ToListAsync();
    }
}