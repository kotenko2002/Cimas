using FluentValidation;

namespace Cimas.Application.Features.Films.Commands.CreateFilm
{
    public class CreateFilmValidator : AbstractValidator<CreateFilmCommand>
    {
        public CreateFilmValidator()
        {
            RuleFor(x => x.CinemaId)
               .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(6);

            RuleFor(x => x.Duration)
                .NotEmpty()
                .InclusiveBetween(0.1, 300);
        }
    }
}
