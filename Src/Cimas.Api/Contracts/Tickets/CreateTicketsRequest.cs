namespace Cimas.Api.Contracts.Tickets
{
    public record CreateTicketsRequest(List<Guid> SeatIds);
}
