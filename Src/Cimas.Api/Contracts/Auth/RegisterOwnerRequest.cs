namespace Cimas.Api.Contracts.Auth
{
    public record RegisterOwnerRequest(
        string CompanyName,
        string FisrtName,
        string LastName,
        string Username,
        string Password
    );
}
