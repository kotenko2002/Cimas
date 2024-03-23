using Cimas.Domain.Entities.Users;
using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Users.Queries.GetCompanyUsers
{
    public class GetCompanyUsersHandler : IRequestHandler<GetCompanyUsersQuery, ErrorOr<List<User>>>
    {
        public Task<ErrorOr<List<User>>> Handle(GetCompanyUsersQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
