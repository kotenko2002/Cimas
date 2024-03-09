using Cimas.Application.Interfaces;
using Cimas.Domain.Entities.Sessions;
using Cimas.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Cimas.Infrastructure.Repositories
{
    public class SessionRepository : BaseRepository<Session>, ISessionRepository
    {
        public SessionRepository(CimasDbContext context) : base(context) {}

        public async Task<Session> GetSessionIncludedTicketsByIdAsync(Guid sessionId)
        {
            return await Sourse
                .Include(session => session.Tickets)
                .FirstOrDefaultAsync(session => session.Id == sessionId);
        }
    }
}
