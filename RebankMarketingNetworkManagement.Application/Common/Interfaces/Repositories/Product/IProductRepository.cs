using RebankMarketingNetworkManagement.Application.Common.Interfaces.Repositories.Base;

namespace RebankMarketingNetworkManagement.Application.Common.Interfaces.Repositories.Product;

public interface IProductRepository : IRepository<Domain.Product>
{
    Task<bool> DoesProductExist(string productCode, string productName);
}