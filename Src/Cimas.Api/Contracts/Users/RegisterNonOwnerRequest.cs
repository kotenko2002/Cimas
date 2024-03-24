namespace Cimas.Api.Contracts.Users
{
    public record RegisterNonOwnerRequest(
        string Username,
        string FisrtName,
        string LastName,
        string Password,
        string Role
    );
}
