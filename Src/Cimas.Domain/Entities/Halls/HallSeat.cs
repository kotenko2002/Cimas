using Cimas.Domain.Entities.Tickets;

namespace Cimas.Domain.Entities.Halls
{
    public class HallSeat : BaseEntity
    {
        public int Number { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public HallSeatStatus Status { get; set; }

        public Guid HallId { get; set; }
        public virtual Hall Hall { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
