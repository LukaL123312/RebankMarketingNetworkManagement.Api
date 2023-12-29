namespace RebankMarketingNetworkManagement.Application.Distributor.Exceptions;

public class RecommendationDepthExceededException : Exception
{
    public RecommendationDepthExceededException(string message)
        : base(message)
    {
    }
}
