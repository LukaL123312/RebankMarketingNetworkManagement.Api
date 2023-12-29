using Microsoft.EntityFrameworkCore;
using RebankMarketingNetworkManagement.Application.Common.Interfaces.Repositories.User;
using RebankMarketingNetworkManagement.Infrastructure.Persistance.Repositories.Base;

namespace RebankMarketingNetworkManagement.Infrastructure.Persistance.Repositories.User;

public class UserRepository
    : Repository<Domain.User>, IUserRepository
{
    public UserRepository(
        AppDbContext context)
        : base(context)
    {
    }

    public Task<bool> DoesUserExist(string userName, string email)
    {
        return DbSet.AnyAsync(u => u.Username == userName || u.Email == email);
    }
}