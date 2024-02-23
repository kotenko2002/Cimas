using Cimas.Application.Interfaces;
using Cimas.Infrastructure.Common;

namespace Cimas.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly CimasDbContext _context;

        public ICompanyRepository CompanyRepository { get; }
        public ICinemaRepository CinemaRepository { get; }

        public UnitOfWork(CimasDbContext context)
        {
            _context = context;

            CompanyRepository = new CompanyRepository(_context);
            CinemaRepository = new CinemaRepository(_context);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
