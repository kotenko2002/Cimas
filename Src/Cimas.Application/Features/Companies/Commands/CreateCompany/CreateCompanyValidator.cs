using FluentValidation;

namespace Cimas.Application.Features.Companies.Commands.CreateCompany
{
    public class CreateCompanyValidator : AbstractValidator<CreateCompanyCommand>
    {
        public CreateCompanyValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(8);
        }
    }
}
