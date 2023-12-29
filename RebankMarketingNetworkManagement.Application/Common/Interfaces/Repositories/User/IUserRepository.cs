using RebankMarketingNetworkManagement.Application.Common.Interfaces.Repositories.Base;

namespace RebankMarketingNetworkManagement.Application.Common.Interfaces.Repositories.User;

public interface IUserRepository : IRepository<Domain.User>
{
    public Task<bool> DoesUserExist(string userName, string email);
}