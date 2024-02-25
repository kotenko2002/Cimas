namespace Cimas.Contracts.Halls
{
    public record UpdateHallSeatsRequst(List<HallSeatModel> Seats);

    public record HallSeatModel(
        Guid Id,
        int Status
    );
}
