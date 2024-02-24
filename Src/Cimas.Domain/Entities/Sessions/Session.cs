using Cimas.Domain.Entities.Films;
using Cimas.Domain.Entities.Halls;

namespace Cimas.Domain.Entities.Sessions
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
