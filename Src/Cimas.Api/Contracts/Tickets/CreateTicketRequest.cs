namespace Cimas.Api.Contracts.Tickets
{
    public record CreateTicketRequest(Guid SessionId, Guid SeatId);
}
