using Cimas.Application.Interfaces;
using Cimas.Domain.Entities.Sessions;
using Cimas.Infrastructure.Common;

namespace Cimas.Infrastructure.Repositories
{
    public class SessionRepository : BaseRepository<Session>, ISessionRepository
    {
        public SessionRepository(CimasDbContext context) : base(context) {}
    }
}
