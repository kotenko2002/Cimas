namespace Cimas.Api.Contracts.Users
{
    public record RegisterNonOwnerRequest(
        string Username,
        string Password,
        string Role
    );
}
