using Cimas.Domain.Entities.Films;
using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Films.Queries.GetFilmsByCinemaId
{
    public record GetFilmsByCinemaIdCommand(
        Guid UserId,
        Guid FilmId
    ) : IRequest<ErrorOr<List<Film>>>;
}
