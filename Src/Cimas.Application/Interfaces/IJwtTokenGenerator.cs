using Cimas.Domain.Auth;
using ErrorOr;
using System.Security.Claims;

namespace Cimas.Application.Interfaces
{
    public interface IJwtTokensService
    {
        TokensPair GenerateTokens(List<Claim> authClaims);
        ErrorOr<ClaimsPrincipal> GetPrincipalFromExpiredToken(string accessToken);
    }
}
