using Cimas.Domain.Entities.Cinemas;
using Cimas.Domain.Entities.Sessions;

namespace Cimas.Domain.Entities.Halls
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
