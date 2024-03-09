using Cimas.Application.Interfaces;
using Cimas.Domain.Entities.Films;
using Cimas.Domain.Entities.Halls;
using Cimas.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Cimas.Infrastructure.Repositories
{
    public class FilmRepository : BaseRepository<Film>, IFilmRepository
    {
        public FilmRepository(CimasDbContext context) : base(context) {}

        public async Task<Film> GetFilmIncludedCinemaByIdAsync(Guid filmId)
        {
            return await Sourse
                .Include(film => film.Cinema)
                .FirstOrDefaultAsync(film => film.Id == filmId);
        }

        public async Task<List<Film>> GetFilmsByCinemaIdAsync(Guid cinemaId)
        {
            return await Sourse
                .Where(hall => !hall.IsDeleted && hall.CinemaId == cinemaId)
                .ToListAsync();
        }
    }
}
