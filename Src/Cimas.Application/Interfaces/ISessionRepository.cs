using Cimas.Domain.Entities.Sessions;

namespace Cimas.Application.Interfaces
{
    public interface ISessionRepository : IBaseRepository<Session>
    {
        Task<Session> GetSessionIncludedTicketsByIdAsync(Guid sessionId);
    }
}
