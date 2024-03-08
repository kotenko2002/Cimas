using Cimas.Domain.Entities.Films;
using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Films.Queries.GetFilmsByCinemaId
{
    public class GetFilmsByCinemaIdHandler : IRequestHandler<GetFilmsByCinemaIdCommand, ErrorOr<List<Film>>>
    {
        public async Task<ErrorOr<List<Film>>> Handle(GetFilmsByCinemaIdCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
