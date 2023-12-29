using MediatR;
using RebankMarketingNetworkManagement.Application.Common.Interfaces.UnitOfWork;
using RebankMarketingNetworkManagement.Application.Common.Security.Jwt.Enums;
using RebankMarketingNetworkManagement.Application.Common.Security.Jwt.Interfaces;
using RebankMarketingNetworkManagement.Application.Common.Security.Password;
using RebankMarketingNetworkManagement.Application.User.Exceptions;

namespace RebankMarketingNetworkManagement.Application.User.Commands.CreateUserCommand;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenService _jwtTokenService;

    public CreateUserCommandHandler(IUnitOfWork unitOfWork, IJwtTokenService jwtTokenService)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userExists = await _unitOfWork.UserRepository.DoesUserExist(request.UserName, request.Email);

        if (userExists)
        {
            throw new UserAlreadyExistsException("User with the specified username or email already exists");
        }

        var (passwordHash, passwordSalt) = PasswordManager.CreatePasswordHash(request.Password);

        var user = new Domain.User
        {
            Username = request.UserName,
            Email = request.Email,
            Password = passwordHash,
            Salt = passwordSalt,
        };

        await _unitOfWork.UserRepository.AddAsync(user, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return await _jwtTokenService.GenerateTokenAsync(user.Id, Role.User);
    }
}