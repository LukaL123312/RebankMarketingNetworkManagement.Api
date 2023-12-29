using MediatR;
using RebankMarketingNetworkManagement.Application.Common.Exceptions;
using RebankMarketingNetworkManagement.Application.Common.Interfaces.UnitOfWork;

namespace RebankMarketingNetworkManagement.Application.Distributor.Commands.DeleteDistributorByIdCommand;

public class DeleteDistributorByIdCommandHandler : IRequestHandler<DeleteDistributorByIdCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDistributorByIdCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Unit> Handle(DeleteDistributorByIdCommand request, CancellationToken cancellationToken)
    {
        var response = await _unitOfWork.DistributorRepository.FindAsync(x => x.DistributorID == request.DistributorID, cancellationToken);

        if (response is null)
        {
            throw new NotFoundException($"Distributor With the Id of || {request.DistributorID} || wasn't found");
        }

        _unitOfWork.DistributorRepository.SoftDelete(response);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}