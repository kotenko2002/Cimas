using Cimas.Domain.Entities.Tickets;
using FluentValidation;
using FluentValidation.Results;

namespace Cimas.Application.Features.Tickets.Commands.UpdateTickets
{
    public class UpdateTicketsValidator : AbstractValidator<UpdateTicketsCommand>
    {
        public UpdateTicketsValidator()
        {
            RuleFor(x => x.Tickets)
                .NotEmpty()
                .Must(Tickets => Tickets.Count > 0)
                .WithMessage("'Tickets' must contain at least 1 tickets")
                .Must(HaveUniqueIds)
                .WithMessage("All seat Ids must be unique")
                .DependentRules(() =>
                {
                    RuleFor(x => x.Tickets)
                    .Custom((seats, context) => AreValidSeats(seats, context));
                });
        }

        private bool HaveUniqueIds(List<UpdateTicketModel> seats)
            => seats.DistinctBy(seat => seat.TicketId).Count() == seats.Count;

        private bool AreValidSeats(List<UpdateTicketModel> seats, ValidationContext<UpdateTicketsCommand> context)
        {
            var invalidSeats = seats.Where(seat => !IsValiSeat(seat)).ToList();

            foreach (var invalidSeat in invalidSeats)
            {
                context.AddFailure(new ValidationFailure(
                    "Tickets",
                    $"Ticket with Id: {invalidSeat.TicketId} is not valid. Please ensure the seat has a valid Id, and has a valid status"));
            }

            return !invalidSeats.Any();
        }
        private bool IsValiSeat(UpdateTicketModel ticket)
            => ticket.TicketId != Guid.Empty && Enum.IsDefined(typeof(TicketStatus), ticket.Status);
    }
}
