using Cimas.Application.Interfaces;
using Cimas.Domain.Cinemas;
using Cimas.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Cimas.Infrastructure.Repositories
{
    public class CinemaRepository : BaseRepository<Cinema>, ICinemaRepository
    {
        public CinemaRepository(CimasDbContext context) : base(context) {}

        public override async Task<Cinema> GetByIdAsync(Guid id)
        {
            return await Sourse
                .Include(cinema => cinema.Company)
                .FirstOrDefaultAsync(cinema => cinema.Id == id);
        }

        public async Task<List<Cinema>> GetCinemasByCompanyIdAsync(Guid companyId)
        {
            return await Sourse
                .Where(cinema => cinema.CompanyId == companyId)
                .ToListAsync();
        }
    }
}
