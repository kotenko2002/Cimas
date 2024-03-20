using FluentValidation;

namespace Cimas.Application.Features.Tickets.Commands.CreateTicket
{
    public class CreateTicketValidator : AbstractValidator<CreateTicketCommand>
    {
        public CreateTicketValidator()
        {
            RuleFor(x => x.Tickets)
                .NotEmpty()
                .Must(tickets => tickets.Count < 1)
                .WithMessage("'Tickets' must contain at least 1 tickets");
        }
    }
}
