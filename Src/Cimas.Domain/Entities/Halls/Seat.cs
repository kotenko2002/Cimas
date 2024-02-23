using Cimas.Domain.Entities;
using Cimas.Domain.Entities.Sessions;

namespace Cimas.Domain.Entities.Halls
{
    public class Seat : BaseEntity
    {
        public int Number { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public SeatStatus Status { get; set; }

        public Guid HallId { get; set; }
        public virtual Hall Hall { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
