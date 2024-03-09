using Cimas.Application.Interfaces;
using Cimas.Domain.Entities.Halls;
using Cimas.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Cimas.Infrastructure.Repositories
{
    public class SeatRepository : BaseRepository<Seat>, ISeatRepository
    {
        public SeatRepository(CimasDbContext context) : base(context) {}

        public async Task<List<Seat>> GetSeatsByIds(IEnumerable<Guid> ids)
        {
            return await Sourse
               .Where(entity => ids.Contains(entity.Id))
               .ToListAsync();
        }
    }
}
