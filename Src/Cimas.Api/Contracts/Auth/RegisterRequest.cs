namespace Cimas.Api.Contracts.Auth
{
    public record RegisterRequest(
        Guid CompanyId,
        string Username,
        string Password,
        string Role
    );
}
