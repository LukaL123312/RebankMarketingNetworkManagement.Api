using RebankMarketingNetworkManagement.Application.DistributorSale.Commands.AddDistributorSaleCommand;
using RebankMarketingNetworkManagement.Domain;

namespace RebankMarketingNetworkManagement.Application.DistributorBonus.Mapping;

public static class DistributorBonusMappingHelper
{
    public static Domain.DistributorBonus ToDistributorBonusEntity(this AddDistributorSaleCommand addDistributorSaleCommand,
        string distributorName, string distributorSurname, decimal recommendationBonus)
    {
        return new Domain.DistributorBonus
        {
            DistributorID = addDistributorSaleCommand.DistributorID,
            DistributorName = distributorName,
            DistributorSurname = distributorSurname
        };
    }

    public static Domain.DistributorBonus UpdateDistributorBonus(this Domain.DistributorBonus existingDistributorBonus,
        Domain.DistributorBonus updatedDistributorBonus)
    {
        existingDistributorBonus.DailyIndividualBonusAmount = existingDistributorBonus.DailyIndividualBonusAmount
            + updatedDistributorBonus.DailyIndividualBonusAmount;
        existingDistributorBonus.DailyFirstGenRecommendationBonusAmount = existingDistributorBonus.DailyFirstGenRecommendationBonusAmount
            + updatedDistributorBonus.DailyFirstGenRecommendationBonusAmount;
        existingDistributorBonus.DailySecondGenRecommendationBonusAmount = existingDistributorBonus.DailySecondGenRecommendationBonusAmount
            + updatedDistributorBonus.DailySecondGenRecommendationBonusAmount;
        existingDistributorBonus.DailySaleAmount = existingDistributorBonus.DailySaleAmount
            + updatedDistributorBonus.DailySaleAmount;

        return existingDistributorBonus;
    }

    public static Domain.DistributorBonus UpdateFirstGenRecommenderDistributorBonus(this Domain.DistributorBonus firstGenRecommenderDistributorBonus,
    Domain.DistributorBonus existingDistributorBonus)
    {
        firstGenRecommenderDistributorBonus.DailyFirstGenRecommendationBonusAmount = existingDistributorBonus.DailySaleAmount / 20;

        return firstGenRecommenderDistributorBonus;
    }

    public static Domain.DistributorBonus UpdateSecondGenRecommenderDistributorBonus(this Domain.DistributorBonus secondGenRecommenderDistributorBonus,
    Domain.DistributorBonus existingDistributorBonus)
    {
        secondGenRecommenderDistributorBonus.DailySecondGenRecommendationBonusAmount = existingDistributorBonus.DailySaleAmount / 100;

        return secondGenRecommenderDistributorBonus;
    }
}
