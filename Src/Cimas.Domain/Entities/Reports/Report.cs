using Cimas.Domain.Entities;
using Cimas.Domain.Entities.WorkDays;

namespace Cimas.Domain.Entities.Reports
{
    public class Report : BaseEntity
    {
        public RepostStatus Status { get; set; }

        public Guid WorkDayId { get; set; }
        public virtual WorkDay WorkDay { get; set; }
    }
}
