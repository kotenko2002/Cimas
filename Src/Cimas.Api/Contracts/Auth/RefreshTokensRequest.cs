namespace Cimas.Api.Contracts.Auth
{
    public record RefreshTokensRequest(
        string AccessToken,
        string RefreshToken
    );
}
