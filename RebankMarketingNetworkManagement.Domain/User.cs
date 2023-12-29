using RebankMarketingNetworkManagement.Domain.Common;

namespace RebankMarketingNetworkManagement.Domain;

public class User : EntityBase
{
    public Guid Id { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Salt { get; set; } = null!;
    public string Email { get; set; } = null!;
}