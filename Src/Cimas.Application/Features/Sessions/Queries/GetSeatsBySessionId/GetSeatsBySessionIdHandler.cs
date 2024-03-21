using Cimas.Application.Interfaces;
using Cimas.Domain.Entities.Halls;
using Cimas.Domain.Entities.Sessions;
using Cimas.Domain.Entities.Users;
using Cimas.Domain.Models.Sessions;
using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Sessions.Queries.GetSeatsBySessionId
{
    public class GetSeatsBySessionIdHandler : IRequestHandler<GetSeatsBySessionIdQuery, ErrorOr<List<SessionSeat>>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ICustomUserManager _userManager;

        public GetSeatsBySessionIdHandler(
            IUnitOfWork uow,
            ICustomUserManager userManager)
        {
            _uow = uow;
            _userManager = userManager;
        }

        public async Task<ErrorOr<List<SessionSeat>>> Handle(GetSeatsBySessionIdQuery query, CancellationToken cancellationToken)
        {
            Session session = await _uow.SessionRepository.GetSessionIncludedTicketsByIdAsync(query.SessionId);
            if (session is null)
            {
                return Error.NotFound(description: "Session with such id does not exist");
            }

            Hall hall = await _uow.HallRepository.GetHallIncludedCinemaAndSeatsByIdAsync(session.HallId);
            User user = await _userManager.FindByIdAsync(query.UserId.ToString());
            if (user.CompanyId != hall.Cinema.CompanyId)
            {
                return Error.Forbidden(description: "You do not have the necessary permissions to perform this action");
            }

            List<SessionSeat> tickets = session.Tickets
                .Select(ticket =>
                {
                    HallSeat seat = hall.Seats.FirstOrDefault(seat => seat.Id == ticket.SeatId);

                    return new SessionSeat()
                    {
                        TicketId = ticket.Id,
                        SeatId = seat.Id,
                        Row = seat.Row,
                        Column = seat.Column,
                        Status = (SessionSeatStatus)ticket.Status
                    };
                })
                .ToList();

            List<SessionSeat> hallSeats = hall.Seats
                .Select(hallSeat => new SessionSeat()
                {
                    SeatId = hallSeat.Id,
                    Row = hallSeat.Row,
                    Column = hallSeat.Column,
                    Status = (SessionSeatStatus)hallSeat.Status
                })
                .ToList();

            List<SessionSeat> sessionSeats = hallSeats
                .ExceptBy(
                    tickets.Select(ticket => ticket.SeatId),
                    sessionSeat => sessionSeat.SeatId)
                .Union(tickets)
                .ToList();

            return sessionSeats;
        }
    }
}
