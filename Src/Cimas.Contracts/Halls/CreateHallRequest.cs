namespace Cimas.Contracts.Halls
{
    public record CreateHallRequest(
        Guid CinemaId,
        string Name,
        int NumberOfRows,
        int NumberOfColumns
    );
}
