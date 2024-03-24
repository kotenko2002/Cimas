using Cimas.Application.Interfaces;
using Cimas.Domain.Entities.Companies;
using Cimas.Domain.Entities.Users;
using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Users.Queries.GetCompanyUsers
{
    public class GetCompanyUsersHandler : IRequestHandler<GetCompanyUsersQuery, ErrorOr<List<User>>>
    {
        private readonly IUnitOfWork _uow;

        public GetCompanyUsersHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<ErrorOr<List<User>>> Handle(GetCompanyUsersQuery query, CancellationToken cancellationToken)
        {
            User owner = await _uow.UserRepository.GetByIdAsync(query.OwnerId);
            
            Company company = await _uow.CompanyRepository.GetCompaniesIncludedUsersByIdAsync(owner.CompanyId);

            return company.Users.ToList();
        }
    }
}
