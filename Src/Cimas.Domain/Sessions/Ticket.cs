using Cimas.Domain.Halls;

namespace Cimas.Domain.Sessions
{
    public class Ticket : BaseEntity
    {
        public DateTime CreationTime { get; set; }

        public Guid SeatId { get; set; }
        public virtual Seat Seat { get; set; }
        public Guid SessionId { get; set; }
        public virtual Session Session { get; set; }
    }
}
