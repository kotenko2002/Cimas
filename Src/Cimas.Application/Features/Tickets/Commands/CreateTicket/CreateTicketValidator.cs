using FluentValidation;

namespace Cimas.Application.Features.Tickets.Commands.CreateTicket
{
    public class CreateTicketValidator : AbstractValidator<CreateTicketCommand>
    {
        public CreateTicketValidator()
        {
            RuleFor(x => x.SessionId)
                .NotEmpty();

            RuleFor(x => x.SeatId)
                .NotEmpty();
        }
    }
}
