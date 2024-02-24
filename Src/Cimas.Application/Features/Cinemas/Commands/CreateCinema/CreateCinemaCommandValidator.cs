using FluentValidation;

namespace Cimas.Application.Features.Cinemas.Commands.CreateCinema
{
    public class CreateCinemaCommandValidator : AbstractValidator<CreateCinemaCommand>
    {
        public CreateCinemaCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(6);

            RuleFor(x => x.Adress)
                .NotEmpty()
                .MinimumLength(6);
        }
    }
}
