using FluentValidation;
using MediatR;
using RebankMarketingNetworkManagement.Application.Distributor.Dtos;
using RebankMarketingNetworkManagement.Domain.Enums;

namespace RebankMarketingNetworkManagement.Application.Distributor.Commands.AddDistributorCommand;

public class AddDistributorCommand : IRequest
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public string? Photo { get; set; } // store base64 encoded version of the photo
    public DistributorPrivateDocumentInformationDto PrivateDocumentInformation { get; set; }
    public DistributorContactInformationDto ContactInformation { get; set; }
    public DistributorAddressInformationDto AddressInformation { get; set; }
    public Guid? RecommenderID { get; set; }
}

