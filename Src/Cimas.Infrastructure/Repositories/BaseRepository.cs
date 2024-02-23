using Cimas.Application.Interfaces;
using Cimas.Domain;
using Cimas.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Cimas.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected CimasDbContext _context;
        protected DbSet<TEntity> Sourse;

        public BaseRepository(CimasDbContext context)
        {
            _context = context;
            Sourse = context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await Sourse.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await Sourse.AddRangeAsync(entities);
        }

        public Task RemoveAsync(TEntity entity)
        {
            Sourse.Remove(entity);
            return Task.CompletedTask;
        }

        public Task RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            Sourse.RemoveRange(entities);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Sourse.ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await Sourse.FindAsync(id);
        }
    }
}
