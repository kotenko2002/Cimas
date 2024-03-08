namespace Cimas.Api.Contracts.Cinemas
{
    public record GetCinemaResponse(
        Guid Id,
        string Name,
        string Adress
    );
}
