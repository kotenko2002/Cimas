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
            Guid? sessionId = GetJointSessionIdOrNull(command.Tickets);
            if(!sessionId.HasValue)
            {
                return Error.Failure(description: "Session Ids are not all the same");
            }

            Session session = await _uow.SessionRepository.GetByIdAsync(sessionId.Value);
            if (session is null)
            {
                return Error.NotFound(description: "Session with such id does not exist");
            }

            List<Guid> seatsIds = command.Tickets.Select(ticket => ticket.SeatId).ToList();
            List<HallSeat> seats = await _uow.SeatRepository.GetSeatsByIdsAsync(seatsIds);
            if (seatsIds.Count != seats.Count)
            {
                return Error.NotFound(description: "One or more seats with such ids does not exist");
            }

            Guid? hallId = GetJointHallIdOrNull(seats);
            if (!hallId.HasValue || session.HallId != hallId.Value)
            {
                return Error.Failure(description: "The seat does not belong to the same hall as the session");
            }

            User user = await _userManager.FindByIdAsync(command.UserId.ToString());
            Hall hall = await _uow.HallRepository.GetHallIncludedCinemaByIdAsync(hallId.Value);
            if (user.CompanyId != hall.Cinema.CompanyId)
            {
                return Error.Forbidden(description: "You do not have the necessary permissions to perform this action");
            }

            // TODO: Check if there is already a ticket for a session with this seat

            List<Ticket> tickets = command.Tickets
                .Select(ticket => new Ticket()
                {
                    CreationTime = DateTime.Now,
                    Session = session,
                    Seat = seats.First(seat => seat.Id == ticket.SeatId)
                }).ToList();

            await _uow.TicketRepository.AddRangeAsync(tickets);

            await _uow.CompleteAsync();

            return Result.Success;
        }

        private Guid? GetJointSessionIdOrNull(List<CreateTicketModel> tickets)
        {
            IEnumerable<Guid> distinctSessionIds = tickets
                .Select(t => t.SessionId)
                .Distinct();

            return distinctSessionIds.Count() == 1 ? distinctSessionIds.First() : null;
        }

        private Guid? GetJointHallIdOrNull(List<HallSeat> seats)
        {
            IEnumerable<Guid> distinctSeatIds = seats
                .Select(t => t.HallId)
                .Distinct();

            return distinctSeatIds.Count() == 1 ? distinctSeatIds.First() : null;
        }
    }
}
