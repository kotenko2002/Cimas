using Cimas.Application.Interfaces;
using Cimas.Domain.Entities.Halls;
using Cimas.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Cimas.Infrastructure.Repositories
{
    public class HallRepository : BaseRepository<Hall>, IHallRepository
    {
        public HallRepository(CimasDbContext context) : base(context) {}

        public async Task<List<Hall>> GetHallsByCinemaId(Guid cinemaId)
        {
            return await Sourse
                .Where(hall => !hall.IsDeleted && hall.CinemaId == cinemaId)
                .ToListAsync();
        }

        public async Task<Guid> GetCompanyIdByHallIdAsync(Guid hallId)
        {
            var hall = await Sourse
                .Include(hall => hall.Cinema)
                    .ThenInclude(cinema => cinema.Company)
                .FirstOrDefaultAsync(hall => hall.Id == hallId);

            return hall.Cinema.Company.Id;
        }
    }
}
