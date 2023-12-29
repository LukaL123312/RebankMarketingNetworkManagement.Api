namespace RebankMarketingNetworkManagement.Application.DistributorBonus.Dtos;

public class DistributorBonusDto
{
    public Guid DistributorID { get; set; }
    public string DistributorName { get; set; }
    public string DistributorSurname { get; set; }

    public decimal? BonusAmountOverTimeSpan { get; set; }
    public DateTime? BonusDate { get; set; }
}
