using Cimas.Domain.Entities.Tickets;

namespace Cimas.Application.Interfaces
{
    public interface ITicketRepository : IBaseRepository<Ticket>
    {
        Task<List<Ticket>> GetTicketsBySessionIdAsync(Guid sessionId);
        Task<List<Ticket>> GetTicketsByIdsAsync(List<Guid> ids);
        Task<bool> TicketsAlreadyExists(Guid sessionId, List<Guid> seatIds);
    }
}
