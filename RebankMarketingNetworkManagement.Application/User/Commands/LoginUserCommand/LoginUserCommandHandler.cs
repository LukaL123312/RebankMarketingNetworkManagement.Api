using MediatR;
using RebankMarketingNetworkManagement.Application.Common.Interfaces.Repositories.User;
using RebankMarketingNetworkManagement.Application.Common.Security.Jwt.Enums;
using RebankMarketingNetworkManagement.Application.Common.Security.Jwt.Interfaces;
using RebankMarketingNetworkManagement.Application.Common.Security.Password;
using System.Security.Authentication;

namespace RebankMarketingNetworkManagement.Application.User.Commands.LoginUserCommand;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
{
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IUserRepository _userRepository;

    public LoginUserCommandHandler(IUserRepository userRepository, IJwtTokenService jwtTokenService)
    {
        _userRepository = userRepository;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindAsync(u => u.Username == request.UserName || request.UserName == u.Email,
            cancellationToken);

        if (user is null || !PasswordManager.IsValidPassword(request.Password, user.Password, user.Salt))
        {
            throw new InvalidCredentialException("Invalid username or password");
        }

        return await _jwtTokenService.GenerateTokenAsync(user.Id, Role.User);
    }
}
