using MediatR;
using RebankMarketingNetworkManagement.Application.Common.Interfaces.UnitOfWork;
using RebankMarketingNetworkManagement.Application.DistributorBonus.Dtos;

namespace RebankMarketingNetworkManagement.Application.DistributorBonus.Queries;

public class GetFilteredDistributorBonusesQueryHandler : IRequestHandler<GetFilteredDistributorBonusesQuery, List<DistributorBonusDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetFilteredDistributorBonusesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<List<DistributorBonusDto>> Handle(GetFilteredDistributorBonusesQuery request, CancellationToken cancellationToken)
    {
        var distributorBonusesOverTimeSpan = await _unitOfWork.DistributorBonusRepository
            .FindAllAsync(x => x.BonusDate >= request.StartDate && x.BonusDate <= request.EndDate);

        var groupedDistributorBonusesOverTimeSpanByDistributorId = distributorBonusesOverTimeSpan.GroupBy(b => b.DistributorID)
           .ToDictionary(group => group.Key, group => group.ToList());

        var groupedSummedDistributorBonusesOverTimeSpanByDistributorId = distributorBonusesOverTimeSpan
            .GroupBy(b => b.DistributorID)
            .ToDictionary(
                group => group.Key,
                group => new
                {
                    DistributorName = group.First().DistributorName,
                    DistributorSurname = group.First().DistributorSurname,
                    BonusByDate = group
                        .GroupBy(d => d.BonusDate)
                        .ToDictionary(
                            dateGroup => dateGroup.Key,
                            dateGroup => dateGroup.Sum(d => d.DailyIndividualBonusAmount
                                + d.DailyFirstGenRecommendationBonusAmount
                                + d.DailySecondGenRecommendationBonusAmount)
                        )
                }
            );

        var distributorBonusDtoList = groupedSummedDistributorBonusesOverTimeSpanByDistributorId
            .SelectMany(distributorGroup => distributorGroup.Value.BonusByDate
                .Select(dateGroup => new DistributorBonusDto
                {
                    DistributorID = distributorGroup.Key,
                    DistributorName = distributorGroup.Value.DistributorName,
                    DistributorSurname = distributorGroup.Value.DistributorSurname,
                    BonusAmountOverTimeSpan = dateGroup.Value,
                })
            )
            .ToList();

        var distributorBonusDtoListWithSum = distributorBonusDtoList
            .GroupBy(dto => dto.DistributorID)
            .Select(group => new DistributorBonusDto
            {
                DistributorID = group.Key,
                DistributorName = group.First().DistributorName,
                DistributorSurname = group.First().DistributorSurname,
                BonusAmountOverTimeSpan = group.Sum(dto => dto.BonusAmountOverTimeSpan ?? 0)
            })
            .ToList();

        if (!string.IsNullOrEmpty(request.DistributorName))
        {
            distributorBonusDtoListWithSum = distributorBonusDtoListWithSum.Where(x => x.DistributorName.ToLower()
            .Equals(request.DistributorName.ToLower())).ToList();
        }

        if (!string.IsNullOrEmpty(request.DistributorSurname))
        {
            distributorBonusDtoListWithSum = distributorBonusDtoListWithSum.Where(x => x.DistributorSurname.ToLower()
            .Equals(request.DistributorSurname.ToLower())).ToList();
        }

        if (request.MinimumValue != null)
        {
            distributorBonusDtoListWithSum = distributorBonusDtoListWithSum.Where(x => x.BonusAmountOverTimeSpan >= request.MinimumValue).ToList();
        }

        if (request.MaximumValue != null)
        {
            distributorBonusDtoListWithSum = distributorBonusDtoListWithSum.Where(x => x.BonusAmountOverTimeSpan <= request.MaximumValue).ToList();
        }

        return distributorBonusDtoListWithSum;
    }
}