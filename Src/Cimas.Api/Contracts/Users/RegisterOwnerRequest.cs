namespace Cimas.Api.Contracts.Users
{
    public record RegisterOwnerRequest(
        string CompanyName,
        string FisrtName,
        string LastName,
        string Username,
        string Password
    );
}
