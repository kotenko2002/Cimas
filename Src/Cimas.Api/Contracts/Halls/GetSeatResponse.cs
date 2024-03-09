using Cimas.Domain.Entities.Halls;

namespace Cimas.Api.Contracts.Halls
{
    public record GetSeatResponse(
        Guid Id,
        int Number,
        int Row,
        int Column,
        SeatStatus Status
    );
}
