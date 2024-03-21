using FluentValidation;

namespace Cimas.Application.Features.Tickets.Commands.CreateTicket
{
    public class CreateTicketValidator : AbstractValidator<CreateTicketCommand>
    {
        public CreateTicketValidator()
        {
            RuleFor(x => x.SeatIds)
                .NotEmpty()
                .Must(seatIds => seatIds.Count > 0)
                .WithMessage("'Tickets' must contain at least 1 tickets");
        }
    }
}
