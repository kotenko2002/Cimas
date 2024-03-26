using FluentValidation;

namespace Cimas.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.CinemaId)
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(6);

            RuleFor(x => x.Price)
                .NotEmpty();
        }
    }
}
