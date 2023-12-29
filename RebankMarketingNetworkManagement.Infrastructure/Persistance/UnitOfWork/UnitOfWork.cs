using RebankMarketingNetworkManagement.Application.Common.Interfaces.Repositories.Distributor;
using RebankMarketingNetworkManagement.Application.Common.Interfaces.Repositories.Product;
using RebankMarketingNetworkManagement.Application.Common.Interfaces.Repositories.User;
using RebankMarketingNetworkManagement.Application.Common.Interfaces.UnitOfWork;
using RebankMarketingNetworkManagement.Infrastructure.Persistance.Repositories.Distributor;
using RebankMarketingNetworkManagement.Infrastructure.Persistance.Repositories.Product;
using RebankMarketingNetworkManagement.Infrastructure.Persistance.Repositories.User;

namespace RebankMarketingNetworkManagement.Infrastructure.Persistance.UnitOfWork;

internal class UnitOfWork : IUnitOfWork
{
    private IUserRepository _userRepository;
    private IDistributorAddressInformationRepository _distributorAddressInformationRepository;
    private IDistributorContactInformationRepository _distributorContactInformationRepository;
    private IDistributorPrivateDocumentInformationRepository _distributorPrivateDocumentInformationRepository;
    private IDistributorRepository _distributorRepository;
    private IDistributorSaleRepository _distributorSaleRepository;
    private IDistributorBonusRepository _distributorBonusRepository;
    private IProductRepository _productRepository;

    private readonly AppDbContext _dbContext;

    public UnitOfWork(
        AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public IUserRepository UserRepository =>
        _userRepository ??= new UserRepository(_dbContext);

    public IDistributorAddressInformationRepository DistributorAddressInformationRepository =>
        _distributorAddressInformationRepository ??= new DistributorAddressInformationRepository(_dbContext);

    public IDistributorContactInformationRepository DistributorContactInformationRepository =>
        _distributorContactInformationRepository ??= new DistributorContactInformationRepository(_dbContext);

    public IDistributorPrivateDocumentInformationRepository DistributorPrivateDocumentInformationRepository =>
        _distributorPrivateDocumentInformationRepository ??= new DistributorPrivateDocumentInformationRepository(_dbContext);

    public IDistributorRepository DistributorRepository =>
        _distributorRepository ??= new DistributorRepository(_dbContext);

    public IDistributorSaleRepository DistributorSaleRepository =>
        _distributorSaleRepository ??= new DistributorSaleRepository(_dbContext);

    public IDistributorBonusRepository DistributorBonusRepository =>
       _distributorBonusRepository ??= new DistributorBonusRepository(_dbContext);

    public IProductRepository ProductRepository =>
        _productRepository ??= new ProductRepository(_dbContext);

    public void SaveChanges() =>
        _dbContext.SaveChanges();

    public Task SaveChangesAsync(
        CancellationToken cancellationToken) =>
        _dbContext.SaveChangesAsync(cancellationToken);

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}
