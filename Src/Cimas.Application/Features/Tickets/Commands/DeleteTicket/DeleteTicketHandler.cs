using Cimas.Application.Interfaces;
using Cimas.Domain.Entities.Sessions;
using Cimas.Domain.Entities.Tickets;
using Cimas.Domain.Entities.Users;
using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Tickets.Commands.DeleteTicket
{
    public class DeleteTicketHandler : IRequestHandler<DeleteTicketCommand, ErrorOr<Success>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ICustomUserManager _userManager;

        public DeleteTicketHandler(
            IUnitOfWork uow,
            ICustomUserManager userManager)
        {
            _uow = uow;
            _userManager = userManager;
        }

        public async Task<ErrorOr<Success>> Handle(DeleteTicketCommand command, CancellationToken cancellationToken)
        {
            List<Ticket> tickets = await _uow.TicketRepository.GetTicketsByIdsAsync(command.TicketIds);
            if (tickets.Count != command.TicketIds.Count)
            {
                return Error.NotFound(description: "One or more tickets with such ids does not exist");
            }

            Guid? sessionId = GetJointSessionIdOrNull(tickets);
            if (!sessionId.HasValue)
            {
                return Error.Failure(description: "Session Ids are not the same in all tickets");
            }

            Session session = await _uow.SessionRepository.GetSessionsIncludedHallThenIncludedCinemaByIdAsync(sessionId.Value);
            User user = await _userManager.FindByIdAsync(command.UserId.ToString());
            if (user.CompanyId != session.Hall.Cinema.CompanyId)
            {
                return Error.Forbidden(description: "You do not have the necessary permissions to perform this action");
            }

            await _uow.TicketRepository.RemoveRangeAsync(tickets);

            await _uow.CompleteAsync();

            return Result.Success;
        }

        private Guid? GetJointSessionIdOrNull(List<Ticket> tickets)
        {
            IEnumerable<Guid> distinctSessionIds = tickets
                .Select(t => t.SessionId)
                .Distinct();

            return distinctSessionIds.Count() == 1 ? distinctSessionIds.First() : null;
        }
    }
}
