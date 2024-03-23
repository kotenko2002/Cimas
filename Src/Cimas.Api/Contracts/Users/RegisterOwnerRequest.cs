namespace Cimas.Api.Contracts.Users
{
    public record RegisterOwnerRequest(
        Guid CompanyId,
        string Username,
        string Password
    );
}
