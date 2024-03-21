using Cimas.Application.Common.Extensions;
using Cimas.Application.Interfaces;
using Cimas.Domain.Entities.Sessions;
using Cimas.Domain.Entities.Tickets;
using Cimas.Domain.Entities.Users;
using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Tickets.Commands.UpdateTickets
{
    public class UpdateTicketsHandler : IRequestHandler<UpdateTicketsCommand, ErrorOr<Success>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ICustomUserManager _userManager;

        public UpdateTicketsHandler(
            IUnitOfWork uow,
            ICustomUserManager userManager)
        {
            _uow = uow;
            _userManager = userManager;
        }

        public async Task<ErrorOr<Success>> Handle(UpdateTicketsCommand command, CancellationToken cancellationToken)
        {
            Dictionary<Guid, TicketStatus> ticketsToUpdate = command.Tickets.ToDictionary(t => t.TicketId, t => t.Status);
            List<Guid> ticketIds = ticketsToUpdate.Keys.ToList();
            List<Ticket> tickets = await _uow.TicketRepository.GetTicketsByIdsAsync(ticketIds);

            if (tickets.Count != ticketIds.Count)
            {
                return Error.NotFound(description: "One or more tickets with such ids does not exist");
            }

            Guid? sessionId = tickets.GetSingleDistinctIdOrNull(ticket => ticket.SessionId);
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

            foreach (var ticket in tickets)
            {
                ticket.Status = ticketsToUpdate[ticket.Id];
            }

            return Result.Success;
        }
    }
}
