using Cimas.Domain.Cinemas;
using Cimas.Domain.Sessions;

namespace Cimas.Domain.Halls
{
    public class Hall : BaseEntity
    {
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public Guid CinemaId { get; set; }
        public virtual Cinema Cinema { get; set; }

        public virtual ICollection<Seat> Seats { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
    }
}
