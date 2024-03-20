using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Tickets.Commands.CreateTicket
{
    public record CreateTicketCommand(
        Guid UserId,
        List<CreateTicketModel> Tickets
    ) : IRequest<ErrorOr<Success>>;

    public record CreateTicketModel(
       Guid SessionId,
       Guid SeatId
    );
}
