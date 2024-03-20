using FluentValidation;

namespace Cimas.Application.Features.Tickets.Commands.DeleteTicket
{
    public class DeleteTicketValidator : AbstractValidator<DeleteTicketCommand>
    {
        public DeleteTicketValidator()
        {
            RuleFor(x => x.TicketIds)
             .NotEmpty()
             .Must(tickets => tickets.Count < 1)
             .WithMessage("'TicketIds' must contain at least 1 Id");
        }
    }
}
