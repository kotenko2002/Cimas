using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Users.Commands.FireUser
{
    public class FireUserHandler : IRequestHandler<FireUserCommand, ErrorOr<Success>>
    {
        public Task<ErrorOr<Success>> Handle(FireUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
