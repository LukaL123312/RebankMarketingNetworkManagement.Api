using MediatR;
using RebankMarketingNetworkManagement.Application.Distributor.Dtos;
using RebankMarketingNetworkManagement.Application.Distributor.Dtos.Update;
using RebankMarketingNetworkManagement.Domain.Enums;

namespace RebankMarketingNetworkManagement.Application.Distributor.Commands.UpdateDistributorByIdCommand;

public class UpdateDistributorByIdCommand : IRequest
{
    public Guid DistributorID { get; set; }
    public string? Name { get; set; } = null!;
    public string? Surname { get; set; } = null!;
    public DateTime? BirthDate { get; set; }
    public Gender? Gender { get; set; }
    public string? Photo { get; set; }
    public DistributorPrivateDocumentInformationUpdateDto? PrivateDocumentInformation { get; set; }
    public DistributorContactInformationUpdateDto? ContactInformation { get; set; }
    public DistributorAddressInformationUpdateDto? AddressInformation { get; set; }
    public Guid? RecommenderID { get; set; }
}
