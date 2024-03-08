using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Films.Commands.CreateFilm
{
    public class CreateFilmHandler : IRequestHandler<CreateFilmCommand, ErrorOr<Success>>
    {
        public async Task<ErrorOr<Success>> Handle(CreateFilmCommand request, CancellationToken cancellationToken)
        {
            return Result.Success;
        }
    }
}
