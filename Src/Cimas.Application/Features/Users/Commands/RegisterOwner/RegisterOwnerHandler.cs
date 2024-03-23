using Cimas.Application.Interfaces;
using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Users.Commands.RegisterOwner
{
    public class RegisterOwnerHandler : IRequestHandler<RegisterOwnerCommand, ErrorOr<Success>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMediator _mediator;

        public RegisterOwnerHandler(
            IUnitOfWork uow, IMediator mediator)
        {
            _uow = uow;
            _mediator = mediator;
        }

        public async Task<ErrorOr<Success>> Handle(RegisterOwnerCommand command, CancellationToken cancellationToken)
        {
            var company = await _uow.CompanyRepository.GetCompaniesIncludedUsersByIdAsync(command.CompanyId);
            if (company is null)
            {
                return Error.NotFound(description: "Company with such id does not exist");
            }

            //if (company.Users.Any(user => user.Ro))
            //{

            //}
            

            throw new NotImplementedException();
        }
    }
}
