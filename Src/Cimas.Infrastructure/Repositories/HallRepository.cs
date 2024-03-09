using Cimas.Application.Interfaces;
using Cimas.Domain.Entities.Cinemas;
using Cimas.Domain.Entities.Halls;
using Cimas.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Cimas.Infrastructure.Repositories
{
    public class HallRepository : BaseRepository<Hall>, IHallRepository
    {
        public HallRepository(CimasDbContext context) : base(context) {}

        public async Task<Hall> GetHallIncludedCinemaByIdAsync(Guid hallId)
        {
            return await Sourse
                .Include(hall => hall.Cinema)
                .FirstOrDefaultAsync(hall => hall.Id == hallId);
        }

        public async Task<List<Hall>> GetHallsByCinemaIdAsync(Guid cinemaId)
        {
            return await Sourse
                .Where(hall => !hall.IsDeleted && hall.CinemaId == cinemaId)
                .ToListAsync();
        }
    }
}
