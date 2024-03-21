using Cimas.Domain.Entities.Tickets;
using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Tickets.Commands.CreateTicket
{
    public record CreateTicketCommand(
        Guid UserId,
        Guid SessionId,
        List<CreateTicketModel> Tickets
    ) : IRequest<ErrorOr<Success>>;

    public record CreateTicketModel(
       Guid SeatId,
       TicketStatus Status
    );
}
