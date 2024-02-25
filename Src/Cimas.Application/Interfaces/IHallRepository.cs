using Cimas.Domain.Entities.Halls;

namespace Cimas.Application.Interfaces
{
    public interface IHallRepository : IBaseRepository<Hall>
    {
        Task<List<Hall>> GetHallsByCinemaIdAsync(Guid cinemaId);
        Task<Guid> GetCompanyIdByHallIdAsync(Guid hallId);
    }
}
