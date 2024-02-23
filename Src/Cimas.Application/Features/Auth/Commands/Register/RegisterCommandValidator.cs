using Cimas.Domain.Users;
using FluentValidation;

namespace Cimas.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .MinimumLength(8);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8);

            RuleFor(x => x.Role)
                .NotEmpty()
                .Must(IsValidRole)
                .WithMessage(GenerateNonValidRoleErrorMessage);
        }

        private bool IsValidRole(string role)
            => Roles.GetRoles().Contains(role);

        private string GenerateNonValidRoleErrorMessage(RegisterCommand command)
        {
            string[] roles = Roles.GetRoles()
                .Select(role => $"'{role}'")
                .ToArray();
            string validRoles = $"{string.Join(", ", roles.Take(roles.Length - 1))} or {roles.Last()}";

            return $"'Role' must be: {validRoles}. You entered '{command.Role}'.";
        }
    }
}
