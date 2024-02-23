using Cimas.Application.Interfaces;
using Cimas.Domain.Users;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Cimas.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<Success>>
    {
        private readonly ICustomUserManager _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly IUnitOfWork _uow;

        public RegisterCommandHandler(
            ICustomUserManager userManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            IUnitOfWork uow)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _uow = uow;
        }

        public async Task<ErrorOr<Success>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            var company = await _uow.CompanyRepository.GetByIdAsync(command.CompanyId);
            if(company is null)
            {
                return Error.NotFound(description: "Company with such id does not exist");
            }

            User existsUser = await _userManager.FindByNameAsync(command.Username);
            if (existsUser is not null)
            {
                return Error.Conflict(description: "User with such username is already exists");
            }

            var user = new User()
            {
                Company = company,
                UserName = command.Username,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            IdentityResult result = await _userManager.CreateAsync(user, command.Password);
            if (!result.Succeeded)
            {
                return result.Errors
                    .Select(e => Error.Validation(
                        code: "Password",
                        description: e.Description))
                    .ToList();
            }

            if(!await _roleManager.RoleExistsAsync(command.Role))
            {
                throw new InvalidOperationException("Role doesn't exists");
            }

            await _userManager.AddToRoleAsync(user, command.Role);

            return Result.Success;
        }
    }
}
