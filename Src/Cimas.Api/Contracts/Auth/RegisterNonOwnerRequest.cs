namespace Cimas.Api.Contracts.Auth
{
    public record RegisterNonOwnerRequest(
        string Username,
        string FisrtName,
        string LastName,
        string Password,
        string Role
    );
}
