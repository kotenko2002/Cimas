using Cimas.Application.Interfaces;
using Cimas.Domain.Entities.WorkDays;
using Cimas.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Cimas.Infrastructure.Repositories
{
    public class WorkdayRepository : BaseRepository<Workday>, IWorkdayRepository
    {
        public WorkdayRepository(CimasDbContext context) : base(context)
        {
        }

        public async Task<Workday> GetWorkdayByUserId(Guid userId)
        {
            return await Sourse
                .Include(workday => workday.User)
                .FirstOrDefaultAsync(workday => workday.UserId == userId && !workday.EndDateTime.HasValue);
        }
    }
}
