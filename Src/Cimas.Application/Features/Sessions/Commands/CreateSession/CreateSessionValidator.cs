using FluentValidation;

namespace Cimas.Application.Features.Sessions.Commands.CreateSession
{
    public class CreateSessionValidator : AbstractValidator<CreateSessionCommand>
    {
        public CreateSessionValidator()
        {
            RuleFor(x => x.HallId)
                .NotEmpty();

            RuleFor(x => x.FilmId)
                .NotEmpty();

            RuleFor(x => x.StartTime)
                .NotEmpty()
                .Must(StartTime => StartTime > DateTime.Now)
                .WithMessage("StartTime must be in the future");
        }
    }
}
