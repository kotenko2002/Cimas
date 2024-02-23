using Cimas.Domain.WorkDays;

namespace Cimas.Domain.Reports
{
    public class Report : BaseEntity
    {
        public RepostStatus Status { get; set; }

        public Guid WorkDayId { get; set; }
        public virtual WorkDay WorkDay { get; set; }
    }
}
