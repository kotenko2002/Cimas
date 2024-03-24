using Cimas.Application.Common.Extensions;
using Cimas.Domain.Entities.Users;
using FluentValidation;

namespace Cimas.Application.Features.Auth.Commands.RegisterNonOwner
{
    public class RegisterNonOwnerValidator : AbstractValidator<RegisterNonOwnerCommand>
    {
        public RegisterNonOwnerValidator()
        {
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
                .Must(role => role.IsNonOwner())
                .WithMessage(GenerateNonValidRoleErrorMessage);
        }

        private string GenerateNonValidRoleErrorMessage(RegisterNonOwnerCommand command)
            => command.Role.GenerateNonValidRoleErrorMessage(Roles.GetNonOwnerRoles());
    }
}
