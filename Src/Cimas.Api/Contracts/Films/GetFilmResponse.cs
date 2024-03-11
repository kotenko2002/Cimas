namespace Cimas.Api.Contracts.Films
{
    public record GetFilmResponse(Guid Id, string Name, TimeSpan Duration);
}
