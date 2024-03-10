using Cimas.Application.Interfaces;
using Cimas.Domain.Entities.Sessions;
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
            //add checks

            List<Session> sessions = await _uow.SessionRepository.GetSessionsByRangeAsync(
                query.CinemaId, query.FromDateTime, query.ToDateTime);

            //mb add cust to SessionResponse or something like that. Or to it in Api(int Adapter/Mapper)

            return sessions;
        }
    }
}
