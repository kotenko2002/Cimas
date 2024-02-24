using Cimas.Domain.Entities.Cinemas;
using Cimas.Domain.Entities.Reports;
using Cimas.Domain.Entities.Users;

namespace Cimas.Domain.Entities.WorkDays
{
    public class WorkDay : BaseEntity
    {
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }

        public Guid CinemaId { get; set; }
        public virtual Cinema Cinema { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public Report Report { get; set; }
    }
}
