using Cimas.Domain.Entities.WorkDays;

namespace Cimas.Application.Interfaces
{
    public interface IWorkdayRepository : IBaseRepository<Workday>
    {
        Task<Workday> GetWorkdayByUserId(Guid userId);
    }
}
