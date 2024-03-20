namespace Cimas.Api.Contracts.Tickets
{
    public record CreateTicketsRequest(List<CreateTicketRequestModel> Tickets);
    public record CreateTicketRequestModel(
        Guid SessionId,
        Guid SeatId
    );
}
