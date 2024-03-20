using Cimas.Application.Interfaces;
using Cimas.Domain.Entities.Tickets;
using Cimas.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Cimas.Infrastructure.Repositories
{
    public class TicketRepository : BaseRepository<Ticket>, ITicketRepository
    {
        public TicketRepository(CimasDbContext context) : base(context) {}

        public async Task<List<Ticket>> GetTicketsBySessionIdAsync(Guid sessionId)
        {
            return await Sourse
                .Where(ticket => ticket.SessionId == sessionId)
                .ToListAsync();
        }

        public async Task<List<Ticket>> GetTicketsByIdsAsync(List<Guid> ids)
        {
            return await Sourse
               .Where(seat => ids.Contains(seat.Id))
               .ToListAsync();
        }
    }
}
