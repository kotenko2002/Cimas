using Cimas.Application.Interfaces;
using Cimas.Domain.Entities.Films;
using Cimas.Domain.Entities.Users;
using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Films.Commands.DeleteFilm
{
    public class DeleteFilmHandler : IRequestHandler<DeleteFilmCommand, ErrorOr<Success>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ICustomUserManager _userManager;

        public DeleteFilmHandler(
            IUnitOfWork uow,
            ICustomUserManager userManager)
        {
            _uow = uow;
            _userManager = userManager;
        }

        public async Task<ErrorOr<Success>> Handle(DeleteFilmCommand command, CancellationToken cancellationToken)
        {
            Film film = await _uow.FilmRepository.GetFilmIncludedCinemaByIdAsync(command.FilmId);
            if (film is null)
            {
                return Error.NotFound(description: "Film with such id does not exist");
            }

            User user = await _userManager.FindByIdAsync(command.UserId.ToString());
            if (user.CompanyId != film.Cinema.CompanyId)
            {
                return Error.Forbidden(description: "You do not have the necessary permissions to perform this action");
            }

            film.IsDeleted = true;

            await _uow.CompleteAsync();

            return Result.Success;
        }
    }
}
