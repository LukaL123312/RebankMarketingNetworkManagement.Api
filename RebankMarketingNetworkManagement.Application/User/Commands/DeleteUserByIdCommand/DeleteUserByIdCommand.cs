using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace RebankMarketingNetworkManagement.Application.User.Commands.DeleteUserByIdCommand;

public class DeleteUserByIdCommand : IRequest
{
    [FromRoute(Name = "userId")]
    public Guid UserId { get; set; }
}
