using FluentValidation;

namespace Cimas.Application.Features.Halls.Queries.GetHallsByCinemaId
{
    public class GetHallsByCinemaIdQueryValidator : AbstractValidator<GetHallsByCinemaIdQuery>
    {
        public GetHallsByCinemaIdQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty();

            RuleFor(x => x.CinemaId)
                .NotEmpty();
        }
    }
}
