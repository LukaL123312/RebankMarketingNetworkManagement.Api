using RebankMarketingNetworkManagement.Domain.Common;

namespace RebankMarketingNetworkManagement.Domain;

public class DistributorBonus : AuditableEntity
{
    public Guid DistributorBonusID { get; set; }
    public decimal? DailySaleAmount { get; set; }
    public decimal? DailyIndividualBonusAmount { get; set; } = 0;
    public decimal? DailyFirstGenRecommendationBonusAmount { get; set; } = 0;
    public decimal? DailySecondGenRecommendationBonusAmount { get; set; } = 0;
    public DateTime? BonusDate { get; set; }
    public Guid DistributorID { get; set; }
    public string DistributorName { get; set; }
    public string DistributorSurname { get; set; }
    public virtual Distributor Distributor { get; set; }
}
