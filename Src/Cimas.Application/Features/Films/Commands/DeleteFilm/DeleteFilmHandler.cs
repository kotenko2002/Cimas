using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Films.Commands.DeleteFilm
{
    public class DeleteFilmHandler : IRequestHandler<DeleteFilmCommand, ErrorOr<Success>>
    {
        public async Task<ErrorOr<Success>> Handle(DeleteFilmCommand request, CancellationToken cancellationToken)
        {
            return Result.Success;
        }
    }
}
