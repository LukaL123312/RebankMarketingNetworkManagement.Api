using MediatR;
using Microsoft.EntityFrameworkCore;
using RebankMarketingNetworkManagement.Application.Common.Interfaces.Repositories.Distributor;
using RebankMarketingNetworkManagement.Domain;
using RebankMarketingNetworkManagement.Infrastructure.Persistance.Repositories.Base;

namespace RebankMarketingNetworkManagement.Infrastructure.Persistance.Repositories.Distributor;

public class DistributorBonusRepository
    : Repository<Domain.DistributorBonus>, IDistributorBonusRepository
{
    public DistributorBonusRepository(
        AppDbContext context)
        : base(context)
    {
    }

    public Task<bool> DoesDistributorBonusExistBySaleDateAndDistributorIdAsync(Guid distributorId, DateTime saleDate)
    {
        return DbSet.AnyAsync(x => x.DistributorID == distributorId && x.CreatedDateTime.Date == saleDate.Date);
    }
}