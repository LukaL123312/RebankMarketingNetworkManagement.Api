using MediatR;
using RebankMarketingNetworkManagement.Application.Common.Exceptions;
using RebankMarketingNetworkManagement.Application.Common.Interfaces.UnitOfWork;
using RebankMarketingNetworkManagement.Application.Distributor.Mapping.Commands;

namespace RebankMarketingNetworkManagement.Application.Distributor.Commands.UpdateDistributorByIdCommand;


public class UpdateDistributorByIdCommandHandler : IRequestHandler<UpdateDistributorByIdCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDistributorByIdCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Unit> Handle(UpdateDistributorByIdCommand request, CancellationToken cancellationToken)
    {
        var distributor = await _unitOfWork.DistributorRepository
            .GetDistributorWithRelatedInformationEntitiesByDistributorIdAsync(request.DistributorID, cancellationToken);

        if (distributor is null)
        {
            throw new NotFoundException($"No distributor with the id of || {request.DistributorID} || exists.");
        }

        if (request.RecommenderID != null)
        {
            var recommender = await _unitOfWork.DistributorRepository
                .FindAsync(x => x.DistributorID == request.RecommenderID.Value, cancellationToken);

            var isDistributorEligible = await _unitOfWork.DistributorRepository.IsDistributorEligibileAsync(recommender, cancellationToken);

            if (isDistributorEligible)
            {
                recommender.RecommendedCount++;

                _unitOfWork.DistributorRepository.Update(recommender);
            }
        }

        _unitOfWork.DistributorRepository.Update(distributor.CommandToEntityMapper(request));

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
