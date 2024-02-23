using Cimas.Domain.Entities;
using Cimas.Domain.Entities.Halls;

namespace Cimas.Domain.Entities.Sessions
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
