using Cimas.Application.Interfaces;
using Cimas.Domain.Entities.Films;
using Cimas.Domain.Entities.Halls;
using Cimas.Domain.Entities.Sessions;
using Cimas.Domain.Entities.Users;
using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Sessions.Commands.CreateSession
{
    public class CreateSessionHandler : IRequestHandler<CreateSessionCommand, ErrorOr<Success>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ICustomUserManager _userManager;

        public CreateSessionHandler(
            IUnitOfWork uow,
            ICustomUserManager userManager)
        {
            _uow = uow;
            _userManager = userManager;
        }

        public async Task<ErrorOr<Success>> Handle(CreateSessionCommand command, CancellationToken cancellationToken)
        {
            Hall hall = await _uow.HallRepository.GetHallIncludedCinemaByIdAsync(command.HallId);
            if (hall is null)
            {
                return Error.NotFound(description: "Hall with such id does not exist");
            }

            Film film = await _uow.FilmRepository.GetByIdAsync(command.FilmId);
            if (film is null)
            {
                return Error.NotFound(description: "Film with such id does not exist");
            }

            if (hall.CinemaId != film.CinemaId)
            {
                return Error.Failure(description: "The film is not being shown in the same cinema as the hall");
            }

            User user = await _userManager.FindByIdAsync(command.UserId.ToString());
            if (user.CompanyId != hall.Cinema.CompanyId)
            {
                return Error.Forbidden(description: "You do not have the necessary permissions to perform this action");
            }

            // TODO: add collision check

            Session session = new Session()
            {
                StartTime = command.StartTime,
                Hall = hall,
                Film = film
            };
            await _uow.SessionRepository.AddAsync(session);

            await _uow.CompleteAsync();

            return Result.Success;
        }
    }
}
