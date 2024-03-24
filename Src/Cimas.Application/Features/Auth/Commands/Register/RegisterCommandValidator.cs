using Cimas.Application.Common.Extensions;
using Cimas.Domain.Entities.Users;
using FluentValidation;

namespace Cimas.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.CompanyId)
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

            RuleFor(x => x.Role)
                .NotEmpty()
                .Must(role => role.IsRoleValid())
                .WithMessage(GenerateNonValidRoleErrorMessage);
        }

        private string GenerateNonValidRoleErrorMessage(RegisterCommand command)
            => command.Role.GenerateNonValidRoleErrorMessage(Roles.GetRoles());
    }
}
