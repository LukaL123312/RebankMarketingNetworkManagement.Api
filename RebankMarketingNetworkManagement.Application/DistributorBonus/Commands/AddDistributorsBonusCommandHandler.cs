using MediatR;
using RebankMarketingNetworkManagement.Application.Common.Exceptions;
using RebankMarketingNetworkManagement.Application.Common.Interfaces.UnitOfWork;
using RebankMarketingNetworkManagement.Application.DistributorBonus.Dtos;
using RebankMarketingNetworkManagement.Application.DistributorBonus.Mapping;
using RebankMarketingNetworkManagement.Application.DistributorSale.Commands.AddDistributorSaleCommand;

namespace RebankMarketingNetworkManagement.Application.DistributorBonus.Commands;

public class AddDistributorsBonusCommandHandler : IRequestHandler<AddDistributorsBonusCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddDistributorsBonusCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    // main logic: we store sum bonus amounts of each distributor for each day, this way we guarantee that no distributor Sale will ever be calculated
    // twice and if we have to get sum bonus amount of a distributor over some time span, we will just sum the sumBonusAmounts over the required days
    // and return that.

    public async Task<Unit> Handle(AddDistributorsBonusCommand request, CancellationToken cancellationToken)
    {
        var eligibleDistributorSales = await _unitOfWork.DistributorSaleRepository.FindAllAsync(x => x.SaleDate >= request.StartDate
        && x.SaleDate <= request.EndDate && !x.IsCountedForBonusCalculation);

        if (eligibleDistributorSales is null)
        {
            return Unit.Value;
        }

        var groupedSalesByDistributorId = eligibleDistributorSales.GroupBy(b => b.DistributorID)
            .ToDictionary(group => group.Key, group => group.ToList());

        var groupedDailySales = eligibleDistributorSales // group sales made by distributors by days inside the range
            .GroupBy(b => new { b.DistributorID, b.SaleDate.Date })
            .ToDictionary(group => group.Key, group => group.ToList());

        // make another grouping and make it so that we now will have a dictionary 
        // of distributorId and a list of summed bonuses for each day

        var sumSalesByDistributorId = groupedDailySales
            .GroupBy(b => b.Key.DistributorID)
            .ToDictionary(
                group => group.Key,
                group => group.ToDictionary(
                    innerGroup => innerGroup.Key.Date,
                    innerGroup => innerGroup.Value.Sum(sale => sale.SumSalePrice)
                )
            );

        var distributorBonusList = sumSalesByDistributorId.SelectMany(kv =>
            kv.Value.Select(innerKv => new Domain.DistributorBonus
            {
                DistributorID = kv.Key,
                DailySaleAmount = innerKv.Value,
                DailyIndividualBonusAmount = innerKv.Value / 10,
                BonusDate = innerKv.Key
            })).ToList();

        ;

        foreach (var distributorBonus in distributorBonusList)
        {
            var distributor = await _unitOfWork.DistributorRepository
                .GetDistributorWithAllRelatedEntitiesByDistributorIdAsync(distributorBonus.DistributorID);

            distributorBonus.DistributorName = distributor.Name;
            distributorBonus.DistributorSurname = distributor.Surname;

            var firstGenRecommendations = new List<Domain.Distributor>();
            var firstGenRecommendationIds = new List<Guid>();

            // Now count first generation recommendation bonuses

            if (distributor.RecommendedDistributors.Any())
            {
                firstGenRecommendations = distributor.RecommendedDistributors.ToList();

                var firstGenRecommendedDistributorIds = distributor.RecommendedDistributors
                        .Select(rd => rd.DistributorID)
                        .ToList();

                firstGenRecommendationIds = firstGenRecommendedDistributorIds;

                var firstGenDistributorBonusListFiltered = distributorBonusList
                        .Where(distributorBonus => firstGenRecommendedDistributorIds.Contains(distributorBonus.DistributorID))
                        .ToList();

                if (firstGenDistributorBonusListFiltered.Any())
                {
                    var firstGenDistributorBonusListFilteredByExactBonusDate = firstGenDistributorBonusListFiltered
                        .Where(x => x.BonusDate == distributorBonus.BonusDate).ToList();

                    if (firstGenDistributorBonusListFilteredByExactBonusDate.Any())
                    {
                        var totalDailyFirstGenRecommendationsSaleAmount = firstGenDistributorBonusListFilteredByExactBonusDate
                            .Sum(distributorBonus => distributorBonus.DailySaleAmount);

                        distributorBonus.DailyFirstGenRecommendationBonusAmount = totalDailyFirstGenRecommendationsSaleAmount / 20;
                    }
                }
            }

            // Now count second generation recommendation bonuses

            if (firstGenRecommendations is null)
            {
                continue;
            }

            var firstGenRecommendationsWithLoadedRelatedEntities = await _unitOfWork.DistributorRepository
                .GetDistributorsWithAllRelatedEntitiesByDistributorIdsAsync(firstGenRecommendationIds);

            var secondGenRecommendedDistributorIds = firstGenRecommendationsWithLoadedRelatedEntities
                .SelectMany(distributor => distributor.RecommendedDistributors.Select(recommended => recommended.DistributorID))
                .ToList();

            var secondGenDistributorBonusListFiltered = distributorBonusList
                        .Where(distributorBonus => secondGenRecommendedDistributorIds.Contains(distributorBonus.DistributorID))
                        .ToList();

            if (secondGenDistributorBonusListFiltered.Any())
            {
                var secondGenDistributorBonusListFilteredByExactBonusDate = secondGenDistributorBonusListFiltered
                    .Where(x => x.BonusDate == distributorBonus.BonusDate).ToList();

                if (secondGenDistributorBonusListFilteredByExactBonusDate.Any())
                {
                    var totalDailySecondGenRecommendationsSaleAmount = secondGenDistributorBonusListFilteredByExactBonusDate
                        .Sum(distributorBonus => distributorBonus.DailySaleAmount);

                    distributorBonus.DailySecondGenRecommendationBonusAmount = totalDailySecondGenRecommendationsSaleAmount / 100;
                }
            }

            var existingDistributorBonus = await _unitOfWork.DistributorBonusRepository
                .FindAsync(x => x.DistributorID == distributorBonus.DistributorID
                && x.BonusDate == distributorBonus.BonusDate);

            if (existingDistributorBonus is null)
            {
                await _unitOfWork.DistributorBonusRepository.AddAsync(distributorBonus);
            }
            else
            {
                var existingBonusDistributor = await _unitOfWork.DistributorRepository
                    .FindAsync(x => x.DistributorID == existingDistributorBonus.DistributorID);

                _unitOfWork.DistributorBonusRepository.Update(existingDistributorBonus.UpdateDistributorBonus(distributorBonus));

                // Now we need to update the first and second Generation recommenders daily bonuses as well

                var firstGenRecommender = await _unitOfWork.DistributorRepository
                    .FindAsync(x => x.DistributorID == existingBonusDistributor.RecommenderID);

                if (firstGenRecommender != null)
                {
                    // Check if the firstGenRecommender has DistriboturBonusRecord on this day

                    var firstGenRecommenderDistributorBonus = await _unitOfWork.DistributorBonusRepository
                        .FindAsync(x => x.DistributorID == firstGenRecommender.DistributorID && x.BonusDate == existingDistributorBonus.BonusDate);

                    // if it is not null we need to simply update it
                    if (firstGenRecommenderDistributorBonus != null)
                    {
                        _unitOfWork.DistributorBonusRepository.Update(firstGenRecommenderDistributorBonus
                            .UpdateFirstGenRecommenderDistributorBonus(existingDistributorBonus));
                    }
                    else // if it is null we need to create it
                    {
                        var firstGenRecommenderDistritbutorBonusToBeCreated = new Domain.DistributorBonus
                        {
                            DistributorID = firstGenRecommender.DistributorID,
                            DistributorName = firstGenRecommender.Name,
                            DistributorSurname = firstGenRecommender.Surname,
                            DailyIndividualBonusAmount = 0,
                            DailySaleAmount = 0,
                            DailyFirstGenRecommendationBonusAmount = existingDistributorBonus.DailyIndividualBonusAmount / 20,
                            DailySecondGenRecommendationBonusAmount = 0,
                            BonusDate = existingDistributorBonus.BonusDate,
                        };

                        await _unitOfWork.DistributorBonusRepository.AddAsync(firstGenRecommenderDistritbutorBonusToBeCreated);
                    }

                    // Now we need to do the same to the second Generation Recommender

                    if (firstGenRecommender.RecommenderID != null)
                    {
                        var secondGenRecommender = await _unitOfWork.DistributorRepository
                            .FindAsync(x => x.DistributorID == firstGenRecommender.RecommenderID);

                        var secondGenRecommenderDistributorBonus = await _unitOfWork.DistributorBonusRepository
                           .FindAsync(x => x.DistributorID == secondGenRecommender.DistributorID && x.BonusDate == existingDistributorBonus.BonusDate);

                        // if it is not null we need to simply update it
                        if (secondGenRecommenderDistributorBonus != null)
                        {
                            _unitOfWork.DistributorBonusRepository.Update(secondGenRecommenderDistributorBonus
                                .UpdateSecondGenRecommenderDistributorBonus(existingDistributorBonus));
                        }
                        else // if it is null we need to create it
                        {
                            var secondGenRecommenderDistritbutorBonusToBeCreated = new Domain.DistributorBonus
                            {
                                DistributorID = secondGenRecommender.DistributorID,
                                DistributorName = secondGenRecommender.Name,
                                DistributorSurname = secondGenRecommender.Surname,
                                DailyIndividualBonusAmount = 0,
                                DailySaleAmount = 0,
                                DailyFirstGenRecommendationBonusAmount = existingDistributorBonus.DailyIndividualBonusAmount / 100,
                                DailySecondGenRecommendationBonusAmount = 0,
                                BonusDate = existingDistributorBonus.BonusDate,
                            };

                            await _unitOfWork.DistributorBonusRepository.AddAsync(secondGenRecommenderDistritbutorBonusToBeCreated);
                        }
                    }
                }
            }
        }

        eligibleDistributorSales = eligibleDistributorSales
            .Select(sale =>
            {
                sale.IsCountedForBonusCalculation = true;
                return sale;
            })
            .ToList();

        _unitOfWork.DistributorSaleRepository.UpdateRange(eligibleDistributorSales);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
