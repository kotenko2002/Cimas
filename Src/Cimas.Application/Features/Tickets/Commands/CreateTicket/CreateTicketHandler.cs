using Cimas.Application.Interfaces;
using Cimas.Domain.Entities.Halls;
using Cimas.Domain.Entities.Sessions;
using Cimas.Domain.Entities.Tickets;
using Cimas.Domain.Entities.Users;
using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Tickets.Commands.CreateTicket
{
    public class CreateTicketHandler : IRequestHandler<CreateTicketCommand, ErrorOr<Success>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ICustomUserManager _userManager;

        public CreateTicketHandler(
            IUnitOfWork uow,
            ICustomUserManager userManager)
        {
            _uow = uow;
            _userManager = userManager;
        }

        public async Task<ErrorOr<Success>> Handle(CreateTicketCommand command, CancellationToken cancellationToken)
        {
            Session session = await _uow.SessionRepository.GetByIdAsync(command.SessionId);
            if (session is null)
            {
                return Error.NotFound(description: "Session with such id does not exist");
            }

            Seat seat = await _uow.SeatRepository.GetByIdAsync(command.SeatId);
            if (seat is null)
            {
                return Error.NotFound(description: "Seat with such id does not exist");
            }

            if(session.HallId != seat.HallId)
            {
                return Error.Failure(description: "The seat does not belong to the same hall as the session");
            }

            User user = await _userManager.FindByIdAsync(command.UserId.ToString());
            Hall hall = await _uow.HallRepository.GetHallIncludedCinemaByIdAsync(session.HallId);
            if (user.CompanyId != hall.Cinema.CompanyId)
            {
                return Error.Forbidden(description: "You do not have the necessary permissions to perform this action");
            }

            // TODO: Check if there is already a ticket for a session with this seat

            Ticket ticket = new Ticket()
            {
                CreationTime = DateTime.Now,
                Session = session,
                Seat = seat
            };
            await _uow.TicketRepository.AddAsync(ticket);

            await _uow.CompleteAsync();

            return Result.Success;
        }
    }
}
