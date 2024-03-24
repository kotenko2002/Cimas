using Cimas.Infrastructure.Common;
using Cimas.Application.Interfaces;
using Cimas.Domain.Entities.Companies;
using Microsoft.EntityFrameworkCore;

namespace Cimas.Infrastructure.Repositories
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(CimasDbContext context) : base(context) {}

        public async Task<Company> GetCompaniesIncludedUsersByIdAsync(Guid companyId)
        {
            return await Sourse
                .Include(company => company.Users)
                .FirstOrDefaultAsync(company => company.Id == companyId);
        }
    }
}
