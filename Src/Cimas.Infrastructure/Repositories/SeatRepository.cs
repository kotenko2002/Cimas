using Cimas.Application.Interfaces;
using Cimas.Domain.Entities.Halls;
using Cimas.Infrastructure.Common;

namespace Cimas.Infrastructure.Repositories
{
    public class SeatRepository : BaseRepository<Seat>, ISeatRepository
    {
        public SeatRepository(CimasDbContext context) : base(context)
        {
        }
    }
}
