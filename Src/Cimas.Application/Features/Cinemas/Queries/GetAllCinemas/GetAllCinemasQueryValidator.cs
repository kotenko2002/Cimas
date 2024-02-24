using FluentValidation;

namespace Cimas.Application.Features.Cinemas.Queries.GetAllCinemas
{
    public class GetAllCinemasQueryValidator : AbstractValidator<GetAllCinemasQuery>
    {
        public GetAllCinemasQueryValidator()
        {
            RuleFor(x => x.UserId)
               .NotEmpty();
        }
    }
}
