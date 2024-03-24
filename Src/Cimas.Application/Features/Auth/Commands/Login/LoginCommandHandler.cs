using Microsoft.AspNetCore.Identity;
using ErrorOr;
using MediatR;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Cimas.Application.Interfaces;
using Cimas.Domain.Entities.Users;
using Cimas.Domain.Models.Auth;

namespace Cimas.Application.Features.Auth.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, ErrorOr<TokensPair>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtTokensService _jwtTokensService;

        public LoginCommandHandler(
            UserManager<User> userManager,
            IJwtTokensService jwtTokensService)
        {
            _userManager = userManager;
            _jwtTokensService = jwtTokensService;
        }

        public async Task<ErrorOr<TokensPair>> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            User user = await _userManager.FindByNameAsync(command.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, command.Password))
            {
                return Error.Unauthorized(description: "Wrong username or password");
            }

            List<Claim> authClaims = await GenerateAuthClaims(user);
            TokensPair tokens = _jwtTokensService.GenerateTokens(authClaims);

            user.RefreshToken = tokens.RefreshToken.Value;
            user.RefreshTokenExpiryTime = tokens.RefreshToken.ValidTo;
            await _userManager.UpdateAsync(user);

            return tokens;
        }

        private async Task<List<Claim>> GenerateAuthClaims(User user)
        {
            var authClaims = new List<Claim>
            {
                new("userId", user.Id.ToString()),
                new(ClaimTypes.Name, user.UserName),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            IList<string> userRoles = await _userManager.GetRolesAsync(user);
            authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            return authClaims;
        }
    }
}
