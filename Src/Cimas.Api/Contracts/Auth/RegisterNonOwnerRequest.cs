namespace Cimas.Api.Contracts.Auth
{
    public record RegisterNonOwnerRequest(
        string FisrtName,
        string LastName,
        string Username,
        string Password,
        string Role
    );
}
