namespace Cimas.Contracts.Cinemas
{
    public record GetCinemaResponse(
        Guid Id,
        string Name,
        string Adress
    );
}
