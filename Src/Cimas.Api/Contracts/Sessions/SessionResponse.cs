namespace Cimas.Api.Contracts.Sessions
{
    public record SessionResponse(
        Guid Id,
        DateTime StartDateTime,
        DateTime EndDateTime,
        string HallName,
        string FilmName
    );
}
