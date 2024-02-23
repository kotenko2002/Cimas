using Cimas.Infrastructure.Common;
using Cimas.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Cimas.Domain.Entities.Companies;

namespace Cimas.Infrastructure.Repositories
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(CimasDbContext context) : base(context) {}
    }
}
