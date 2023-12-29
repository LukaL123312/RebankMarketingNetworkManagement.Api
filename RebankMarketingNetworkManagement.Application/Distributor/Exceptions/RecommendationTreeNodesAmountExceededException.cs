namespace RebankMarketingNetworkManagement.Application.Distributor.Exceptions;

public class RecommendationTreeNodesAmountExceededException : Exception
{
    public RecommendationTreeNodesAmountExceededException(string message)
        : base(message)
    {
    }
}
