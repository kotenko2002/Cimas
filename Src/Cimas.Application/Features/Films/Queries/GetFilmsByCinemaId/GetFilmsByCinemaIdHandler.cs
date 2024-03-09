using Cimas.Application.Interfaces;
using Cimas.Domain.Entities.Cinemas;
using Cimas.Domain.Entities.Films;
using Cimas.Domain.Entities.Users;
using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Films.Queries.GetFilmsByCinemaId
{
    public class GetFilmsByCinemaIdHandler : IRequestHandler<GetFilmsByCinemaIdQuery, ErrorOr<List<Film>>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ICustomUserManager _userManager;

        public GetFilmsByCinemaIdHandler(
            IUnitOfWork uow,
            ICustomUserManager userManager)
        {
            _uow = uow;
            _userManager = userManager;
        }

        public async Task<ErrorOr<List<Film>>> Handle(GetFilmsByCinemaIdQuery query, CancellationToken cancellationToken)
        {
            Cinema cinema = await _uow.CinemaRepository.GetByIdAsync(query.CinemaId);
            if (cinema is null)
            {
                return Error.NotFound(description: "Cinema with such id does not exist");
            }

            User user = await _userManager.FindByIdAsync(query.UserId.ToString());
            if (user.CompanyId != cinema.CompanyId)
            {
                return Error.Forbidden(description: "You do not have the necessary permissions to perform this action");
            }

            return await _uow.FilmRepository.GetFilmsByCinemaIdAsync(query.CinemaId);
        }
    }
}
