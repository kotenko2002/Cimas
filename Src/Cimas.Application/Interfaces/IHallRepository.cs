using Cimas.Domain.Entities.Halls;

namespace Cimas.Application.Interfaces
{
    public interface IHallRepository : IBaseRepository<Hall>
    {
        Task<Hall> GetHallIncludedCinemaByIdAsync(Guid hallId);
        Task<List<Hall>> GetHallsByCinemaIdAsync(Guid cinemaId);
    }
}
