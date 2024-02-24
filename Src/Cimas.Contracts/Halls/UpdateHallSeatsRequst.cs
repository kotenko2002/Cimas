namespace Cimas.Contracts.Halls
{
    public record UpdateHallSeatsRequst(List<UpdateHallSeat> seats);

    public record UpdateHallSeat(
        Guid Id,
        int Status
    );
}
