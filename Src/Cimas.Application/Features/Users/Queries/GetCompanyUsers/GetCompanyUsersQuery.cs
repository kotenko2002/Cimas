using Cimas.Domain.Entities.Users;
using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Users.Queries.GetCompanyUsers
{
    public record GetCompanyUsersQuery(
        Guid OwnerId    
    ) : IRequest<ErrorOr<List<User>>>;
}
