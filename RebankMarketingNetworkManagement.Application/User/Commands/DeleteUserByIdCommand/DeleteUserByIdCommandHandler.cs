using MediatR;
using RebankMarketingNetworkManagement.Application.Common.Exceptions;
using RebankMarketingNetworkManagement.Application.Common.Interfaces.UnitOfWork;

namespace RebankMarketingNetworkManagement.Application.User.Commands.DeleteUserByIdCommand;

public class DeleteUserByIdHandler : IRequestHandler<DeleteUserByIdCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Unit> Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
    {
        var response = await _unitOfWork.UserRepository.FindAsync(x => x.Id == request.UserId, cancellationToken);

        if (response is null)
        {
            throw new NotFoundException($"User With the Id of || {request.UserId} || wasn't found");
        }

        _unitOfWork.UserRepository.SoftDelete(response);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
