using Cimas.Domain.Cinemas;
using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Cinemas.Commands.CreateCinema
{
    public record CreateCinemaCommand(
        Guid UserId,
        string Name,
        string Adress
    ) : IRequest<ErrorOr<Cinema>>;
}
