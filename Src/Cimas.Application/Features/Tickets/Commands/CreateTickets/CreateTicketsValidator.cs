using Cimas.Domain.Entities.Tickets;
using FluentValidation;
using FluentValidation.Results;

namespace Cimas.Application.Features.Tickets.Commands.CreateTickets
{
    public class CreateTicketsValidator : AbstractValidator<CreateTicketsCommand>
    {
        public CreateTicketsValidator()
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

        private bool HaveUniqueIds(List<CreateTicketModel> seats)
           => seats.DistinctBy(seat => seat.SeatId).Count() == seats.Count;

        private bool AreValidSeats(List<CreateTicketModel> seats, ValidationContext<CreateTicketsCommand> context)
        {
            var invalidSeats = seats.Where(seat => !IsValiSeat(seat)).ToList();

            foreach (var invalidSeat in invalidSeats)
            {
                context.AddFailure(new ValidationFailure(
                    "Seats",
                    $"Seat with Id: {invalidSeat.SeatId} is not valid. Please ensure the seat has a valid Id, and has a valid status"));
            }

            return !invalidSeats.Any();
        }
        private bool IsValiSeat(CreateTicketModel ticket)
            => ticket.SeatId != Guid.Empty && Enum.IsDefined(typeof(TicketStatus), ticket.Status);
    }
}
