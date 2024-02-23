namespace Cimas.Contracts.Auth
{
    public record RefreshTokensRequest(
        string AccessToken,
        string RefreshToken
    );
}
