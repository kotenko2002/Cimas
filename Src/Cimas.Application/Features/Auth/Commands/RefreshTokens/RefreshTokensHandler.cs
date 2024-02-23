using Cimas.Application.Interfaces;
using Cimas.Domain.Auth;
using Cimas.Domain.Users;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Cimas.Application.Features.Auth.Commands.RefreshTokens
{
    internal class RefreshTokensHandler : IRequestHandler<RefreshTokensCommand, ErrorOr<TokensPair>>
    {
        private readonly ICustomUserManager _userManager;
        private readonly IJwtTokensService _jwtTokensService;

        public RefreshTokensHandler(
            ICustomUserManager userManager,
            IJwtTokensService jwtTokensService)
        {
            _userManager = userManager;
            _jwtTokensService = jwtTokensService;
        }

        public async Task<ErrorOr<TokensPair>> Handle(RefreshTokensCommand command, CancellationToken cancellationToken)
        {
            ErrorOr<ClaimsPrincipal> getPrincipalResult = 
                _jwtTokensService.GetPrincipalFromExpiredToken(command.AccessToken);
            if(getPrincipalResult.IsError)
            {
                return getPrincipalResult.Errors;
            }

            string username = getPrincipalResult.Value.Identity.Name;
            User user = await _userManager.FindByNameAsync(username);
            if (user == null || user.RefreshToken != command.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return Error.Failure(description: "Invalid access token or refresh token");
            }

            List<Claim> authClaims = getPrincipalResult.Value.Claims.ToList();
            TokensPair tokens = _jwtTokensService.GenerateTokens(authClaims);

            user.RefreshToken = tokens.RefreshToken.Value;
            user.RefreshTokenExpiryTime = tokens.RefreshToken.ValidTo;
            await _userManager.UpdateAsync(user);

            return tokens;
        }
    }
}
