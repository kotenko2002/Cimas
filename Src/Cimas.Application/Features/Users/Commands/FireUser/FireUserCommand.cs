using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Users.Commands.FireUser
{
    public record FireUserCommand(
        Guid OwnerUserId,
        Guid UserToDeleteId
    ) : IRequest<ErrorOr<Success>>;
}
