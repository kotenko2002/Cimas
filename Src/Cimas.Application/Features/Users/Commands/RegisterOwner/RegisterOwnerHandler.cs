using Cimas.Application.Features.Auth.Commands.Register;
using Cimas.Application.Interfaces;
using Cimas.Domain.Entities.Companies;
using Cimas.Domain.Entities.Users;
using ErrorOr;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Cimas.Application.Features.Users.Commands.RegisterOwner
{
    public class RegisterOwnerHandler : IRequestHandler<RegisterOwnerCommand, ErrorOr<Success>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMediator _mediator;
        private readonly UserManager<User> _userManager;

        public RegisterOwnerHandler(
            IUnitOfWork uow,
            IMediator mediator,
            UserManager<User> userManager)
        {
            _uow = uow;
            _mediator = mediator;
            _userManager = userManager;
        }

        public async Task<ErrorOr<Success>> Handle(RegisterOwnerCommand command, CancellationToken cancellationToken)
        {
            Company company = await _uow.CompanyRepository.GetCompaniesIncludedUsersByIdAsync(command.CompanyId);
            if (company is null)
            {
                return Error.NotFound(description: "Company with such id does not exist");
            }

            List<User> activeEmployees = company.Users.Where(user => !user.IsFired).ToList();
            foreach (User user in activeEmployees)
            {
                IList<string> userRoles = await _userManager.GetRolesAsync(user);

                if(userRoles.Contains(Roles.Owner))
                {
                    return Error.Forbidden(description: "The company already has an owner");
                }
            }

            var registerCommand = (company, Roles.Owner, command).Adapt<RegisterCommand>();

            return await _mediator.Send(registerCommand);
        }
    }
}
