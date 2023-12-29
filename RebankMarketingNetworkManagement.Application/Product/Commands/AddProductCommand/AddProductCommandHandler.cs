using MediatR;
using RebankMarketingNetworkManagement.Application.Common.Interfaces.UnitOfWork;
using RebankMarketingNetworkManagement.Application.Product.Mapping;

namespace RebankMarketingNetworkManagement.Application.Product.Commands.AddProductCommand;

public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Unit> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.ProductRepository.AddAsync(request.CommandToEntityMapper(), cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}