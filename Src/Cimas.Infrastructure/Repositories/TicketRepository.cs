using Cimas.Application.Interfaces;
using Cimas.Domain.Entities.Tickets;
using Cimas.Infrastructure.Common;

namespace Cimas.Infrastructure.Repositories
{
    public class TicketRepository : BaseRepository<Ticket>, ITicketRepository
    {
        public TicketRepository(CimasDbContext context) : base(context)
        {
        }
    }
}
