using Cimas.Domain.Cinemas;

namespace Cimas.Application.Interfaces
{
    public interface ICinemaRepository : IBaseRepository<Cinema>
    {
        Task<List<Cinema>> GetCinemasByCompanyIdAsync(Guid companyId);
    }
}
