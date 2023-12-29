using RebankMarketingNetworkManagement.Application.Common.Interfaces.Repositories.Distributor;
using RebankMarketingNetworkManagement.Application.Common.Interfaces.Repositories.Product;
using RebankMarketingNetworkManagement.Application.Common.Interfaces.Repositories.User;

namespace RebankMarketingNetworkManagement.Application.Common.Interfaces.UnitOfWork;

public interface IUnitOfWork
{
    public IUserRepository UserRepository { get; }
    public IDistributorAddressInformationRepository DistributorAddressInformationRepository { get; }
    public IDistributorContactInformationRepository DistributorContactInformationRepository { get; }
    public IDistributorPrivateDocumentInformationRepository DistributorPrivateDocumentInformationRepository { get; }
    public IDistributorRepository DistributorRepository { get; }
    public IDistributorSaleRepository DistributorSaleRepository { get; }
    public IDistributorBonusRepository DistributorBonusRepository { get; }
    public IProductRepository ProductRepository { get; }
    void SaveChanges();
    Task SaveChangesAsync(CancellationToken cancellationToken);
    public void Dispose();
}