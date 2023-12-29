using MediatR;
using RebankMarketingNetworkManagement.Application.Common.Exceptions;
using RebankMarketingNetworkManagement.Application.Common.Interfaces.UnitOfWork;
using RebankMarketingNetworkManagement.Application.Distributor.Mapping.Commands;
using RebankMarketingNetworkManagement.Application.DistributorBonus.Mapping;
using RebankMarketingNetworkManagement.Application.DistributorSale.Mapping;

namespace RebankMarketingNetworkManagement.Application.DistributorSale.Commands.AddDistributorSaleCommand;


public class AddDistributorSaleCommandHandler : IRequestHandler<AddDistributorSaleCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddDistributorSaleCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Unit> Handle(AddDistributorSaleCommand request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.ProductRepository.FindAsync(x => x.ProductCode.ToLower().Equals(request.ProductCode.ToLower())
        && x.ProductName.ToLower().Equals(request.ProductName.ToLower()));

        if (product is null)
        {
            throw new NotFoundException($"No product with the productCode of || {request.ProductCode} ||" +
                $" and productName of || {request.ProductName} || exists.");
        }

        var distributor = await _unitOfWork.DistributorRepository.GetDistributorWithAllRelatedEntitiesByDistributorIdAsync(request.DistributorID);

        if (distributor is null)
        {
            throw new NotFoundException($"No distributor with the id of || {request.DistributorID} || exists.");
        }

        await _unitOfWork.DistributorSaleRepository.AddAsync(request.CommandToEntityMapper(product.ProductID), cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}