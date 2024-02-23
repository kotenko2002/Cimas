using Cimas.Domain.Auth;
using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Auth.Commands.RefreshTokens
{
    public record RefreshTokensCommand(
        string AccessToken,
        string RefreshToken
    ) : IRequest<ErrorOr<TokensPair>>;
}
