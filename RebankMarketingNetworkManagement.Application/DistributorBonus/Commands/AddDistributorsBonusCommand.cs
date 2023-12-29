using MediatR;

namespace RebankMarketingNetworkManagement.Application.DistributorBonus.Commands;

public class AddDistributorsBonusCommand : IRequest
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}