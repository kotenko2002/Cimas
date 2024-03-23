using ErrorOr;
using MediatR;

namespace Cimas.Application.Features.Users.Commands.RegisterOwner
{
    public record RegisterOwnerCommand(
        Guid CompanyId,
        string Username,
        string Password,
        string Role
    ) : IRequest<ErrorOr<Success>>;
}
