using Cimas.Domain.Entities.Halls;

namespace Cimas.Application.Interfaces
{
    public interface ISeatRepository : IBaseRepository<Seat>
    {
        Task<List<Seat>> GetSeatsByIds(IEnumerable<Guid> ids);
    }
}
