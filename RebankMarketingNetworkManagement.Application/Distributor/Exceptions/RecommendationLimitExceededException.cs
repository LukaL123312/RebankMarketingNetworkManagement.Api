namespace RebankMarketingNetworkManagement.Application.Distributor.Exceptions;

public class RecommendationLimitExceededException : Exception
{
    public RecommendationLimitExceededException(string message)
        : base(message)
    {
    }
}
