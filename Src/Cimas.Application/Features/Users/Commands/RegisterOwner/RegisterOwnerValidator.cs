using FluentValidation;

namespace Cimas.Application.Features.Users.Commands.RegisterOwner
{
    public class RegisterOwnerValidator : AbstractValidator<RegisterOwnerCommand>
    {
        public RegisterOwnerValidator()
        {
            RuleFor(x => x.CompanyName)
                .NotEmpty();

            RuleFor(x => x.FisrtName)
                .NotEmpty();

            RuleFor(x => x.LastName)
                .NotEmpty();

            RuleFor(x => x.Username)
                .NotEmpty()
                .MinimumLength(6);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6);
        }
    }
}
