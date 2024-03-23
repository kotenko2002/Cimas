using Cimas.Application.Features.Auth.Commands.Register;
using Cimas.Application.Interfaces;
using Cimas.Domain.Entities.Companies;
using Cimas.Domain.Entities.Users;
using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Users.Commands.RegisterNonOwner
{
    public class RegisterNonOwnerHandler : IRequestHandler<RegisterNonOwnerCommand, ErrorOr<Success>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMediator _mediator;

        public RegisterNonOwnerHandler(
            IUnitOfWork uow, IMediator mediator)
        {
            _uow = uow;
            _mediator = mediator;
        }

        public async Task<ErrorOr<Success>> Handle(RegisterNonOwnerCommand request, CancellationToken cancellationToken)
        {
            User owner = await _uow.UserRepository.GetByIdAsync(request.OwnerUserId);

            Company company = await _uow.CompanyRepository.GetByIdAsync(owner.CompanyId);

            var command = new RegisterCommand(
                company,
                request.Username,
                request.Password,
                request.Role);

            return await _mediator.Send(command);
        }
    }
}
