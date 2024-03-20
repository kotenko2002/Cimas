using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Tickets.Commands.DeleteTicket
{
    public record DeleteTicketCommand(
        Guid UserId,
        List<Guid> TicketIds
    ) : IRequest<ErrorOr<Success>>;
}
