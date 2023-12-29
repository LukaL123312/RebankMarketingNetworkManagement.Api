using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace RebankMarketingNetworkManagement.Application.Distributor.Commands.DeleteDistributorByIdCommand;

public class DeleteDistributorByIdCommand : IRequest
{
    [FromRoute(Name = "distributorId")]
    public Guid DistributorID { get; set; }
}