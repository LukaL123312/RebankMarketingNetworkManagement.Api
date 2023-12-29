namespace RebankMarketingNetworkManagement.Application.Distributor.Exceptions;

public class DistributorAlreadyExistsException : Exception
{
    public DistributorAlreadyExistsException(string message)
        : base(message)
    {
    }
}
