using Cimas.Domain.Cinemas;
using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Cinemas.Queries.GetCinemaById
{
    public record GetCinemaQuery(
        Guid UserId,
        Guid CinemaId
    ) : IRequest<ErrorOr<Cinema>>;
}
