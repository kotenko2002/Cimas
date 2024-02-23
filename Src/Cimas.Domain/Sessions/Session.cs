using Cimas.Domain.Films;
using Cimas.Domain.Halls;

namespace Cimas.Domain.Sessions
{
    public class Session : BaseEntity
    {
        public DateTime StartTime { get; set; }

        public Guid HallId { get; set; }
        public virtual Hall Hall { get; set; }
        public Guid FilmId { get; set; }
        public virtual Film Film { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
