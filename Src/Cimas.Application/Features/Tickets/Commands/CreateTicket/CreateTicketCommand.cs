using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Tickets.Commands.CreateTicket
{
    public record CreateTicketCommand(
        Guid UserId,
        Guid SessionId,
        Guid SeatId
    ) : IRequest<ErrorOr<Success>>;
}
