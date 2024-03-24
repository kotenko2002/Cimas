using Cimas.Application.Features.Auth.Commands.Register;
using Cimas.Application.Features.Companies.Commands.CreateCompany;
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
            var createCompanyCommand = new CreateCompanyCommand(command.CompanyName);
            ErrorOr<Company> createCompanyResult = await _mediator.Send(createCompanyCommand);

            if (createCompanyResult.IsError)
            {
                return createCompanyResult.Errors;
            }

            Guid companyId = createCompanyResult.Value.Id;

            var registerCommand = (companyId, Roles.Owner, command).Adapt<RegisterCommand>();
            return await _mediator.Send(registerCommand);
        }
    }
}
