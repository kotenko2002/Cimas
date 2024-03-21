using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Tickets.Commands.CreateTicket
{
    public record CreateTicketCommand(
        Guid UserId,
        Guid SessionId,
        List<Guid> SeatIds
    ) : IRequest<ErrorOr<Success>>;
}
