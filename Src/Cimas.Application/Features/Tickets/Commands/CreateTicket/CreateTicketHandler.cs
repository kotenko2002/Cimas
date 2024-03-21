using Cimas.Application.Common.Extensions;
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

            List<Guid> seatIds = command.Tickets.Select(ticket => ticket.SeatId).ToList();
            List<HallSeat> seats = await _uow.SeatRepository.GetSeatsByIdsAsync(seatIds);
            if (seatIds.Count != seats.Count)
            {
                return Error.NotFound(description: "One or more seats with such ids does not exist");
            }

            if(seats.Any(seat => seat.Status != HallSeatStatus.Available))
            {
                return Error.Failure(description: "One or more seats with such ids are not avalible");
            }

            Guid? hallId = seats.GetSingleDistinctIdOrNull(seat => seat.HallId);
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

            bool isTicketsAlreadyExists = await _uow.TicketRepository.IsTicketsAlreadyExists(
                session.Id,
                seatIds);
            if (isTicketsAlreadyExists)
            {
                return Error.Failure(description: "Ticket for one of the seats is already sold out");
            }

            List<Ticket> tickets = command.Tickets
                .Select(ticket => new Ticket()
                {
                    Session = session,
                    Seat = seats.First(seat => seat.Id == ticket.SeatId),
                    CreationTime = DateTime.UtcNow,
                    Status = ticket.Status
                }).ToList();

            await _uow.TicketRepository.AddRangeAsync(tickets);

            await _uow.CompleteAsync();

            return Result.Success;
        }
    }
}
