using Cimas.Application.Interfaces;
using Cimas.Domain.Entities.Cinemas;
using Cimas.Domain.Entities.Sessions;
using Cimas.Domain.Entities.Users;
using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Sessions.Queries.GetSessionsByRange
{
    public class GetSessionsByRangeHandler : IRequestHandler<GetSessionsByRangeQuery, ErrorOr<List<Session>>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ICustomUserManager _userManager;

        public GetSessionsByRangeHandler(
            IUnitOfWork uow,
            ICustomUserManager userManager)
        {
            _uow = uow;
            _userManager = userManager;
        }

        public async Task<ErrorOr<List<Session>>> Handle(GetSessionsByRangeQuery query, CancellationToken cancellationToken)
        {
            Cinema cinema = await _uow.CinemaRepository.GetByIdAsync(query.CinemaId);
            if (cinema is null)
            {
                return Error.NotFound(description: "Cinema with such id does not exist");
            }

            User user = await _userManager.FindByIdAsync(query.UserId.ToString());
            if (user.CompanyId != cinema.CompanyId)
            {
                return Error.Forbidden(description: "You do not have the necessary permissions to perform this action");
            }

            List<Session> sessions = await _uow.SessionRepository.GetSessionsByRangeAsync(
                query.CinemaId, query.FromDateTime, query.ToDateTime);

            return sessions;
        }
    }
}
