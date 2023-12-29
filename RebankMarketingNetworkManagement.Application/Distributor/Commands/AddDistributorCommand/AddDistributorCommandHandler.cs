using MediatR;
using RebankMarketingNetworkManagement.Application.Common.Exceptions;
using RebankMarketingNetworkManagement.Application.Common.Interfaces.UnitOfWork;
using RebankMarketingNetworkManagement.Application.Distributor.Exceptions;
using RebankMarketingNetworkManagement.Application.Distributor.Mapping.Commands;
using RebankMarketingNetworkManagement.Application.User.Commands.DeleteUserByIdCommand;
using RebankMarketingNetworkManagement.Application.User.Exceptions;

namespace RebankMarketingNetworkManagement.Application.Distributor.Commands.AddDistributorCommand;

internal class AddDistributorCommandHandler : IRequestHandler<AddDistributorCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddDistributorCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Unit> Handle(AddDistributorCommand request, CancellationToken cancellationToken)
    {
        var distributorExists = await _unitOfWork.DistributorRepository
            .DoesDistributorExistByPrivateNumberAsync(request.PrivateDocumentInformation.PrivateNumber);

        if (distributorExists)
        {
            throw new DistributorAlreadyExistsException("Distributor with the specified Private Number already exists");
        }

        if (request.RecommenderID != null)
        {
            var recommender = await _unitOfWork.DistributorRepository
                .FindAsync(x => x.DistributorID == request.RecommenderID.Value, cancellationToken);

            var isDistributorEligible = await _unitOfWork.DistributorRepository.IsDistributorEligibileAsync(recommender, cancellationToken);

            if(isDistributorEligible) // if not eligible, exception will be thrown
            {
                recommender.RecommendedCount++;

                _unitOfWork.DistributorRepository.Update(recommender);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
        }

        // if nobody recommended a distributor, we do not need to check for additional business logic constraints

        await _unitOfWork.DistributorRepository.AddAsync(request.CommandToEntityMapper(), cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
