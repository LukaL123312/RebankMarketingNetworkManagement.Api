using RebankMarketingNetworkManagement.Application.Common.Security.Jwt.Enums;

namespace RebankMarketingNetworkManagement.Application.Common.Security.Jwt.Interfaces;

public interface IJwtTokenService
{
    Task<string> GenerateTokenAsync(
        Guid userId,
        Role role);
}
